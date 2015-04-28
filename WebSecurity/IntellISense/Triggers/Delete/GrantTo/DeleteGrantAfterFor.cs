//using System;
//using System.Collections.Generic;
//using System.Data.Entity.Infrastructure;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using IntellISenseSecurity;
//using WebSecurity.IntellISense.Common;
//using WebSecurity.IntellISense.Delete;
//using WebSecurity.IntellISense.Grant;
//
//namespace WebSecurity.IntellISense.Triggers.Delete.GrantTo
//{
//    public class DeleteGrantAfterFor //: ICommandTermTrigger
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
//                        typeof (CommandTermSecObjectName),
//                        typeof (CommandTermFor)
//                    }
//                };
//            }
//        }
//
//        public Action<CommandTermStack> Trigger
//        {
//            get { return TriggerActions.DeleteGrantAfterFor; }
//        }
//    }
//}
