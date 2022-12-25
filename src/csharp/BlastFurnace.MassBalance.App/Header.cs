using BlastFurnace.MassBalance.Lib;

namespace BlastFurnace.MassBalance.ConsoleApp
{
    internal class Header
    {
        private readonly string descriptiveText = About.Description;

        public string DescriptiveText { get { return descriptiveText; } }

        private readonly string appTitle = About.AppTitle;

        public string AppTitle { get { return appTitle; } }

        public Header() 
        { 

        }
    }
}
