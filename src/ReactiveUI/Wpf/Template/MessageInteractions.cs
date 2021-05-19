using System.Reactive;
using ReactiveUI;

namespace Template
{
    public static class MessageInteractions
    {
        public static Interaction<string, Unit> ShowMessage { get; } = new Interaction<string, Unit>();
    }
}