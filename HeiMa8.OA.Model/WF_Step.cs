//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace HeiMa8.OA.Model
{
    using System;
    using System.Collections.Generic;
    
    [Serializable]
    public partial class WF_Step
    {
        public int ID { get; set; }
        public string StepName { get; set; }
        public int ProcessBy { get; set; }
        public System.DateTime SubTime { get; set; }
        public System.DateTime ProcessTime { get; set; }
        public string ProcessResult { get; set; }
        public string ProcessContent { get; set; }
        public short StepStaus { get; set; }
        public bool IsStartStep { get; set; }
        public bool IsEndStep { get; set; }
        public int ParentStepId { get; set; }
        public int WF_InstanceID { get; set; }
    
        public virtual WF_Instance WF_Instance { get; set; }
    }
}
