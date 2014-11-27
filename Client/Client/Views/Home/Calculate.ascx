<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Client.Calculator.CalcData>" %>

<%
    
    Writer.Write("{{CurValue: {0}, PrevNum: {1}, CurSign: \"{2}\", Flag: {3}, Flagequal: {4}, MemoryValue: {5},  InputValue: \"{6}\" }}",
                 Model.CurValue.ToString().Replace(',', '.'), Model.PrevNum.ToString().Replace(',', '.'), Model.CurSign, Model.Flag.ToString().ToLower(), Model.Flagequal.ToString().ToLower(), Model.MemoryValue, Model.InputValue.Replace(',', '.'));
%>