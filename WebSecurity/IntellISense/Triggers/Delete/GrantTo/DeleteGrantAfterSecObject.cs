//using System;
//using IntellISenseSecurity;
//using WebSecurity.IntellISense.Common;
//using WebSecurity.IntellISense.Delete;
//using WebSecurity.IntellISense.Grant;
//
//namespace WebSecurity.IntellISense.Triggers.Delete.GrantTo
//{
//    internal class DeleteGrantAfterSecObject //: ICommandTermTrigger
//    {
//        public Type[][] CommandTermTypes
//        {
//            get
//            {
//                return new[]
//                {
//                    new[]
//                    {
//                        typeof (CommandTermDelete),
//                        typeof (CommandTermGrantTo),
//                        typeof (CommandTermRoleName),
//                        typeof (CommandTermOn),
//                        typeof (CommandTermSecObjectName)
//                    }
//                };
//            }
//        }
//
//        public Action<CommandTermStack> Trigger
//        {
//            get { return TriggerActions.DeleteGrantAfterSecObject; }
//        }
//    }
//}
