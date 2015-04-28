//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using IntellISenseSecurity;
//using WebSecurity.IntellISense.Common;
//using WebSecurity.IntellISense.Delete;
//
//namespace WebSecurity.IntellISense.Triggers.Delete.GrantTo
//{
//    public class DeleteGrantAfterOn //: ICommandTermTrigger
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
//                    }
//                };
//            }
//        }
//
//        public Action<CommandTermStack> Trigger
//        {
//            get { return TriggerActions.DeleteGrantAfterOn; }
//        }
//    }
//}
