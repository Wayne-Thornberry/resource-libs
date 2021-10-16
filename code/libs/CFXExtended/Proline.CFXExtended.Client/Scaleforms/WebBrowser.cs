

namespace Proline.CFXExtended.Core.Scaleforms
{
    public class WebBrowser : ScaleformUI
    {
        public WebBrowser() : base("web_browser")
        {

        }

        public void SetMouseInput(float mouseX, float mouseY)
        {
            CallFunction("SET_MOUSE_INPUT", mouseX, mouseY);
        }

        public void InitiliseWebsite()
        {
            CallFunction("INITIALISE_WEBSITE");
        }

        public void GoToWebpage(string page = "WWW_SOUTHERNSANANDREASSUPERAUTOS_COM")
        {
            CallFunction("GO_TO_WEBPAGE", page);
        }

        public void EnableAllButtons()
        {
            CallFunction("ENABLE_ALL_BUTTONS");
        }
    }
}