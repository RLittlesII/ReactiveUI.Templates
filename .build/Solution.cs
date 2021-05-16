using Nuke.Common;
using Nuke.Common.CI.GitHubActions;
using Nuke.Common.Git;
using Nuke.Common.IO;
using Nuke.Common.Tools.GitVersion;
using Rocket.Surgery.Nuke;
using Rocket.Surgery.Nuke.ContinuousIntegration;
using Rocket.Surgery.Nuke.DotNetCore;
using Rocket.Surgery.Nuke.GithubActions;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

[GitHubActionsSteps("templates", GitHubActionsImage.MacOsLatest,
    AutoGenerate = false,
    On = new[] { GitHubActionsTrigger.Push },
    OnPushTags = new[] { "v*" },
    OnPushBranches = new[] { "master", "next" },
    OnPullRequestBranches = new[] { "master", "next" },
    InvokedTargets = new[] { nameof(Default) },
    NonEntryTargets = new[]
    {
        nameof(ICIEnvironment.CIEnvironment),
        nameof(ITriggerCodeCoverageReports.Trigger_Code_Coverage_Reports),
        nameof(ITriggerCodeCoverageReports.Generate_Code_Coverage_Report_Cobertura),
        nameof(IGenerateCodeCoverageBadges.Generate_Code_Coverage_Badges),
        nameof(IGenerateCodeCoverageReport.Generate_Code_Coverage_Report),
        nameof(IGenerateCodeCoverageSummary.Generate_Code_Coverage_Summary),
        nameof(Default)
    },
    ExcludedTargets = new[] { nameof(ICanClean.Clean), nameof(ICanRestoreWithDotNetCore.DotnetToolRestore) })]
class Solution : NukeBuild,
    ICanRestoreWithDotNetCore,
    ICanBuildWithDotNetCore,
    ICanTestWithDotNetCore,
    ICanPackWithDotNetCore,
    IComprehendTemplates,
    IHaveDataCollector,
    ICanClean,
    ICanUpdateReadme,
    IGenerateCodeCoverageReport,
    IGenerateCodeCoverageSummary,
    IGenerateCodeCoverageBadges,
    IHaveConfiguration<Configuration>,
    ICanLint,
    ICanInstallDotNetTemplates
{

    /// <summary>
    /// Support plugins are available for:
    /// - JetBrains ReSharper        https://nuke.build/resharper
    /// - JetBrains Rider            https://nuke.build/rider
    /// - Microsoft VisualStudio     https://nuke.build/visualstudio
    /// - Microsoft VSCode           https://nuke.build/vscode
    /// </summary>
    public static int Main() => Execute<Solution>(x => x.Default);

    [OptionalGitRepository]
    public GitRepository? GitRepository { get; }

    private Target Default => _ => _
        .DependsOn(Restore)
        .DependsOn(Build)
        .DependsOn(Test)
        .DependsOn(Pack)
        .DependsOn(Install)
        .DependsOn(InstallForTest)
    ;

    public Target Build => _ => _.Inherit<ICanBuildWithDotNetCore>(x => x.CoreBuild);
    public Target Install => _ => _.Inherit<ICanInstallDotNetTemplates>(x => x.Install);
    public Target InstallForTest => _ => _.Inherit<ICanInstallDotNetTemplates>(x => x.InstallForTest);

    public Target Pack => _ => _.Inherit<ICanPackWithDotNetCore>(x => x.CorePack)
        .DependsOn(Clean);

    [ComputedGitVersion]
    public GitVersion GitVersion { get; } = null!;

    public Target Clean => _ => _.Inherit<ICanClean>(x => x.Clean);
    public Target Restore => _ => _.Inherit<ICanRestoreWithDotNetCore>(x => x.CoreRestore);
    public Target Test => _ => _.Inherit<ICanTestWithDotNetCore>(x => x.CoreTest);

    public Target BuildVersion => _ => _.Inherit<IHaveBuildVersion>(x => x.BuildVersion)
        .Before(Default)
        .Before(Clean);

    [Parameter("Configuration to build")]
    public Configuration Configuration { get; } = IsLocalBuild ? Configuration.Debug : Configuration.Release;
}

public interface ICanInstallDotNetTemplates : IHaveTestTarget, IHaveBuildTarget, IHavePackTarget, IHaveNuGetPackages,
    IComprehendSources, INukeBuild
{
    public Target InstallForTest => _ => _
        .Before(Test)
        .DependsOn(Build)
        .Executes(() =>
        {
            DotNet($"new --install {SourceDirectory}");
        });

    public Target Install => _ => _
        .After(Pack)
        .DependsOn(Pack)
        .OnlyWhenStatic(() => NukeBuild.IsLocalBuild)
        .Executes(() =>
        {
            DotNet($"new --uninstall {SourceDirectory}");
            foreach (var item in ((IHaveNuGetPackages) this).NuGetPackageDirectory.GlobFiles("*.nupkg"))
            {

                try
                {
                    DotNet("new --uninstall Rocket.Surgery.Templates");
                }
                catch
                {
                }

                DotNet($"new --install {item}");
            }
        });
}
