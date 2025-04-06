using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace AuthenticationSystemApi.Models
{
    [ExcludeFromCodeCoverage]
    public class Variables
    {
        private readonly Dictionary<string, object?> VariablesContent = new();

        private T? GetValue<T>([CallerMemberName] string key = "") =>
            VariablesContent.ContainsKey(key) ? (T?)VariablesContent[key] : default;

        private void SetValue<T>(T? value, [CallerMemberName] string key = "")
        {
            if (VariablesContent.ContainsKey(key))
            {
                VariablesContent[key] = value;
                return;
            }
            VariablesContent.Add(key, value);
        }

        public bool SampleBool { get => GetValue<bool>(); set => SetValue(value); }

        public Example? ExampleVariable { get => GetValue<Example>(); set => SetValue(value); }

        public IEnumerable<Example>? ExamplesVariables { get => GetValue<IEnumerable<Example>>(); set => SetValue(value); }
    }

    public class Example
    {
    }
}
