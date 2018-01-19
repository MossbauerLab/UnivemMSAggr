using System;
using System.Windows.Markup;

namespace MossbauerLab.UnivemMsAggr.GUI.Views.Extension
{
    [MarkupExtensionReturnType(typeof(String))]
    public class EnvironmentVariableMarkupExtension : MarkupExtension
    {
        public EnvironmentVariableMarkupExtension(String variableName)
        {
            if(String.IsNullOrEmpty(variableName))
                throw new ArgumentNullException("variableName");
            VariableName = variableName;
        }

        public override Object ProvideValue(IServiceProvider serviceProvider)
        {
            return Environment.GetEnvironmentVariable(VariableName);
        }

        [ConstructorArgument("variableName")]
        public String VariableName { get; set; }
    }
}
