<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Home Page
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="style" runat="server">
    <style type="text/css">
     input
     {
      height:40px;
     }
   </style>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="server">
   <script type="text/javascript">
       var CalcData = {};
       CalcData["CurValue"] = 0;
       CalcData["PrevNum"] = 0;
       CalcData["CurSign"] = "null";
       CalcData["Flag"] = true;
       CalcData["Flagequal"] = false;
       CalcData["MemoryValue"] = 0;
       CalcData["InputValue"] = "";

       var dict = {};
       dict["/"] = "divide";
       dict["*"] = "multiply";
       dict["-"] = "minus";
       dict["."] = "dot";
       dict["="] = "equal";
       dict["+"] = "plus";
       dict["M+"] = "MP";
       dict["+/-"] = "ChangeSign";

       window.onload = onld;

       function onld() {
           var str = "";

           var arr = [["MC", "7", "8", "9", "/"], ["MR", "4", "5", "6", "*"], ["MS", "1", "2", "3", "-"], ["M+", "0", ".", "=", "+"]];

           for (var i = 0; i < 4; i++) {
               var line = "<tr>";
               for (var j = 0; j < 5; j++) {
                   line += "<td>";
                   line += "<input type='button' onclick='iter(\"clck\", this.value)' style='width:40px' value='" + arr[i][j] + "'>";
                   line += "</td>";
               }
               line += "</tr>";
               str = str + line;
           }

           var tbl = document.getElementById("tbl");
           tbl.innerHTML = str;
           var fr = document.getElementById("fr");
           fr.style.width = tbl.clientWidth;
           document.getElementById("inpt").style.width = tbl.clientWidth - 5;
       }

       function iter(method, btn) {
           var hk = "";
           var elem = document.getElementById("inpt");
           CalcData["InputValue"] = elem.value;

           for (var label in CalcData) {
               CalcData[label] = CalcData[label].toString().replace('.', ',');
               hk += "&" + label + "=" + CalcData[label];
           }
           var txt = sendRequest("Home/Calculate", "method=" + method + "&btn=" + ((dict[btn]) ? dict[btn] : btn) + hk);

           eval("CalcData = " + txt);

           elem.value = CalcData["InputValue"];
       }

       function sendRequest(NameFile, param) {
           try {
               var xmlhttp = new XMLHttpRequest();
               xmlhttp.open("POST", NameFile, false);
               xmlhttp.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
               xmlhttp.send(param);

               var ans = xmlhttp.responseText;

               return (xmlhttp.status == 200) ? ans : "";
           }
           catch (e) {
               alert(e.toString());
           }
       }
   </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   <table style="margin:auto">
       <tr><td>
          <table style="width:100%">
              <tr><td>
                   <input type="text" id="inpt" style="width:100%;direction:rtl;font-size:18px;" onchange="normalize(this)" maxlength="10" value="0" disabled="disabled" size="23"/>
              </td></tr>
          </table>
       </td></tr>
       <tr><td>
           <table border="0" id="fr" style="width:100%">
              <tr>
                 <td>
                   <input type="button" style="width:100%" value="<-" onclick="iter('bckspc', this.value)"/>
                 </td>
                 <td>
                   <input type="button" style="width:100%" value="CE" onclick="iter('ce', this.value)"/>
                 </td>
                 <td>
                   <input type="button" style="width:100%" value="C" onclick="iter('c', this.value)"/>
                 </td>
              </tr>
              <tr>
                 <td>
                   <input type="button" style="width:100%" value="power" onclick="iter('clck', this.value)"/>
                 </td>
                 <td>
                   <input type="button" style="width:100%" value="sqrt" onclick="iter('clck', this.value)"/>
                 </td>
                 <td>
                   <input type="button" style="width:100%" value="+/-" onclick="iter('clck', this.value)"/>
                 </td>
              </tr>
           </table>
       </td></tr>
       <tr><td>
           <table border="0" id="tbl">
           </table>
       </td></tr>
   </table>
</asp:Content>
