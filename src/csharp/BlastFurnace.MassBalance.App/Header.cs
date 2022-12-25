using System.Text;

using BlastFurnace.MassBalance.Lib;

namespace BlastFurnace.MassBalance.ConsoleApp;

internal class Header
{
    private readonly string mainDivider = "===============================================================================";

    private readonly string secondaryDivider = "_______________________________________________________________________________";

    private readonly string descriptiveText = About.Description;

    public string DescriptiveText
    {
        get
        {
            var sb = new StringBuilder();
            sb.AppendLine(secondaryDivider);
            sb.AppendLine(descriptiveText);
            sb.AppendLine(secondaryDivider);
            return sb.ToString();
        }
    }

    private readonly string appTitle = About.AppTitle;

    public string AppTitle
    {
        get
        {
            var sb = new StringBuilder();
            sb.AppendLine(mainDivider);
            sb.AppendLine(appTitle);
            sb.AppendLine(mainDivider);
            return sb.ToString();
        }
    }

    public Header()
    {

    }
}
