using Newtonsoft.Json;
using System;
using CitizenFX.Core.Native;
using Proline.ClassicOnline.Engine.Parts;
using Proline.Resource.IO;
using System.Collections.Generic;
using CitizenFX.Core;
using TestTool.ButtonPresses;
using System.Xml.Linq;
using HTMLBuilder.Tool;
using HTMLBuilder.Lib.Elements;

namespace TestTool
{
    public class TestToolProgram
    {
        public static void Main(string[] args)
        {
            var tool = new ToolWindow();
            tool.SetupBootstrap();

            tool.Head.Title = "Woah";
            tool.Head.AddStyle("html,         body {             background-color: transparent;         }");
            tool.Head.AddStyle("\r\n        #mydiv {\r\n            position: absolute;\r\n            z-index: 9;\r\n            background-color: #f1f1f1;\r\n            border: 1px solid #d3d3d3;\r\n            text-align: center;\r\n        }\r\n\r\n        #mydivheader {\r\n            padding: 10px;\r\n            cursor: move;\r\n            z-index: 10;\r\n            background-color: #2196F3;\r\n            color: #fff;\r\n        }");
            var tElement = new TextElement();
            tElement.Text = "Random";
            tElement.Id = "AnId";
            tElement.Class = "text-justify";
            var div = new DivElement();
            div.Id = "mydiv";
            div.Class = "container";
            div.Add(tElement);
            tool.AddToBody(div);
            tool.Head.AddScript("function onClickHandler() {             fetch(`https://${GetParentResourceName()}/testCallback`, {                 method: 'POST',                 headers: {                     'Content-Type': 'application/json; charset=UTF-8',                 },                 body: JSON.stringify({                     itemId: 'my-item'                 })             }).then(resp => resp.json()).then(resp => console.log(resp));         };");
            tool.Body.AddScript("dragElement(document.getElementById(\"mydiv\"));\r\n\r\n        function dragElement(elmnt) {\r\n            var pos1 = 0, pos2 = 0, pos3 = 0, pos4 = 0;\r\n            if (document.getElementById(elmnt.id + \"header\")) {\r\n                // if present, the header is where you move the DIV from:\r\n                document.getElementById(elmnt.id + \"header\").onmousedown = dragMouseDown;\r\n            } else {\r\n                // otherwise, move the DIV from anywhere inside the DIV:\r\n                elmnt.onmousedown = dragMouseDown;\r\n            }\r\n\r\n            function dragMouseDown(e) {\r\n                e = e || window.event;\r\n                e.preventDefault();\r\n                // get the mouse cursor position at startup:\r\n                pos3 = e.clientX;\r\n                pos4 = e.clientY;\r\n                document.onmouseup = closeDragElement;\r\n                // call a function whenever the cursor moves:\r\n                document.onmousemove = elementDrag;\r\n            }\r\n\r\n            function elementDrag(e) {\r\n                e = e || window.event;\r\n                e.preventDefault();\r\n                // calculate the new cursor position:\r\n                pos1 = pos3 - e.clientX;\r\n                pos2 = pos4 - e.clientY;\r\n                pos3 = e.clientX;\r\n                pos4 = e.clientY;\r\n                // set the element's new position:\r\n                elmnt.style.top = (elmnt.offsetTop - pos2) + \"px\";\r\n                elmnt.style.left = (elmnt.offsetLeft - pos1) + \"px\";\r\n            }\r\n\r\n            function closeDragElement() {\r\n                // stop moving when mouse button is released:\r\n                document.onmouseup = null;\r\n                document.onmousemove = null;\r\n            }\r\n        }");
            var btn = new ButtonElement();
            btn.ClickedEvent += OnClick;

            var nav = new NavElement();
            nav.Id = "mydivheader";
            nav.Class = "navbar navbar-expand-lg navbar-light bg-light";
            nav.Value = "";
            var link = new LinkElement();
            link.Href = "https://www.google.com";
            link.Text = "A Link";
            link.Class = "navbar-brand";
            nav.Add(link);

            var list = new ListElement();
            list.Add("dsad");
            list.Add("ewee");
            list.Add("das");
            btn.Class = "btn btn-primary";
            btn.Text = "Wow";
            div.Add(nav);
            div.Add(btn);
            div.Add(list);
            var fileContents = tool.Build();
            EngineAPI.LogDebug(fileContents);



            API.RegisterNuiCallbackType("testCallback");
            var em = Proline.Resource.Eventing.EventManager.GetInstance();

            var ec = new TestEventCallback();
            var close = new CloseProgramEvent();

            ec.Subscribe(new Action<IDictionary<string, object>, CallbackDelegate>((data, cb) =>
            {
                EngineAPI.LogDebug("Woah1!!!!");
                cb("Hey it worked");
            }));


            var html = new XElement("html");

            close.Subscribe(new Action<IDictionary<string, object>, CallbackDelegate>((data, cb) =>
            {
                API.SetNuiFocus(false, false);
                cb("Hey it worked");
            }));

            var json = JsonConvert.SerializeObject(new
            {
                transactionType = "d",
                transactionFile = "demo",
                transactionVolume = 0.2f,
                jsonHTML = fileContents
            });
            API.SendNuiMessage(json);
        }

        private static void OnClick(object sender, ClickEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
