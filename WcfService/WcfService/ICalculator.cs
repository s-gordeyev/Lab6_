using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfService
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени интерфейса "ICalculator" в коде и файле конфигурации.
    [ServiceContract]
    public interface ICalculator
    {

        [OperationContract]
        string GetData(int value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        [OperationContract]
        CalcData clck(CalcData cd, string btn);

        [OperationContract]
        void memory(CalcData cd, string action);

        [OperationContract]
        void act(CalcData cd, string curSign_);

        [OperationContract]
        void setValue(CalcData cd, double value);

        [OperationContract]
        double getValue(CalcData cd);

        [OperationContract]
        CalcData bckspc(CalcData cd);

        [OperationContract]
        CalcData ce(CalcData cd);

        [OperationContract]
        CalcData c(CalcData cd);

        // TODO: Добавьте здесь операции служб
    }


    // Используйте контракт данных, как показано в примере ниже, чтобы добавить составные типы к операциям служб.
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }

    [DataContract]
    public class CalcData
    {
        double curValue = 0;
        double prevNum = 0;
        string curSign = null;
        bool flag = true;
        bool flagequal = false;
        double memoryValue = 0;
        string inputValue = "";

        [DataMember]
        public double CurValue
        {
            get { return curValue; }
            set { curValue = value; }
        }

        [DataMember]
        public double PrevNum 
        {
            get { return prevNum; }
            set { prevNum = value; }
        }

        [DataMember]
        public string CurSign
        {
            get { return curSign; }
            set { curSign = value; }
        }

        [DataMember]
        public bool Flag
        {
            get { return flag; }
            set { flag = value; }
        }

        [DataMember]
        public bool Flagequal
        {
            get { return flagequal; }
            set { flagequal = value; }
        }

        [DataMember]
        public double MemoryValue
        {
            get { return memoryValue; }
            set { memoryValue = value; }
        }

        [DataMember]
        public string InputValue
        {
            get { return inputValue; }
            set { inputValue = value; }
        }
    }
}
