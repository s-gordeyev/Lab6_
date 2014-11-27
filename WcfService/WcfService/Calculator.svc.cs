using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfService
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "Calculator" в коде, SVC-файле и файле конфигурации.
    public class Calculator : ICalculator
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }


        public CalcData clck(CalcData cd, string btn)
        {
            btn = (btn == "dot") ? "." : btn;

            if ("0123456789.".IndexOf(btn) > -1)
            {
                if (!cd.Flag)
                {
                    cd.InputValue = btn;
                    cd.Flag = true;
                }
                else
                {
                    if (cd.InputValue == "0" && btn != ".")
                        cd.InputValue = btn;
                    else
                        cd.InputValue = cd.InputValue + btn;
                }
            }
            else if (btn == "sqrt")
            {
                var hk = getValue(cd);
                this.setValue(cd, Math.Sqrt(hk));
            }
            else if (btn == "ChangeSign")
            {
                var hk = getValue(cd);
                this.setValue(cd, hk * (-1));
            }
            else if (btn.IndexOf("M") == 0)
                this.memory(cd, btn);
            else
            {
                this.act(cd, btn);
                cd.Flag = false;
            }

            return cd;
        }


        public void memory(CalcData cd, string action)
        {
            switch (action)
            {
                case "MC":
                    cd.MemoryValue = 0;
                    break;
                case "MR":
                    setValue(cd, cd.MemoryValue);
                    cd.Flag = true;
                    break;
                case "MS":
                    cd.MemoryValue = getValue(cd);
                    break;
                case "MP":
                    cd.MemoryValue += getValue(cd);
                    break;
            }
        }


        public void act(CalcData cd, string curSign_)
        {
            if (cd.Flagequal && curSign_ != "equal")
            {
                cd.CurSign = null;
                cd.PrevNum = 0;
                cd.Flagequal = false;
            }

            double newValue = this.getValue(cd);
            if (cd.Flag)
                cd.PrevNum = newValue;

            switch (cd.CurSign)
            {
                case "plus":
                    cd.CurValue = cd.CurValue + cd.PrevNum;
                    this.setValue(cd, cd.CurValue);
                    break;
                case "minus":
                    cd.CurValue  = cd.CurValue  - cd.PrevNum;
                    this.setValue(cd, cd.CurValue);
                    break;
                case "multiply":
                    cd.CurValue  = cd.CurValue  * cd.PrevNum;
                    this.setValue(cd, cd.CurValue);
                    break;
                case "divide":
                    cd.CurValue  = cd.CurValue  / cd.PrevNum;
                    this.setValue(cd, cd.CurValue);
                    break;
                case "power":
                    cd.CurValue  = Math.Pow(cd.CurValue , cd.PrevNum);
                    this.setValue(cd, cd.CurValue);
                    break;
                default:
                    cd.CurValue  = newValue;
                    break;
            }


            if (curSign_ != "equal")
                cd.CurSign = curSign_;
            else
                cd.Flagequal = true;
        }


        public void setValue(CalcData cd, double value)
        {
            if (value < 0)
                cd.InputValue = value * (-1) + "-";
            else
                cd.InputValue = value.ToString();
        }


        public double getValue(CalcData cd)
        {
            try
            {
                if (cd.InputValue.IndexOf("-") > -1)
                    return Double.Parse(cd.InputValue.Substring(0, cd.InputValue.Length - 1)) * (-1);
                else
                    return Double.Parse(cd.InputValue);
            }
            catch
            {
                cd.InputValue = cd.InputValue.Replace('.', ',');
                if (cd.InputValue.IndexOf("-") > -1)
                    return Double.Parse(cd.InputValue.Substring(0, cd.InputValue.Length - 1)) * (-1);
                else
                    return Double.Parse(cd.InputValue);
            }
        }


        public CalcData bckspc(CalcData cd)
        {
            if (cd.InputValue.Length > 0)
            {
                if (cd.InputValue.IndexOf("-") == -1)
                    cd.InputValue = cd.InputValue.Substring(0, cd.InputValue.Length - 1);
                else
                    cd.InputValue = cd.InputValue.Substring(0, cd.InputValue.Length - 2) + "-";
            }

            if (cd.InputValue.Length == 0 || cd.InputValue == "-")
                cd.InputValue = "0";

            return cd;
        }


        public CalcData ce(CalcData cd)
        {
            cd.InputValue = "0";
            return cd;
        }


        public CalcData c(CalcData cd)
        {
            cd.CurSign = "null";
            cd.PrevNum = 0;
            cd.InputValue = "0";
            cd.CurValue = 0;
            cd.Flagequal = false;
            cd.Flag = true;
            cd.MemoryValue = 0;
            return cd;
        }
    }
}
