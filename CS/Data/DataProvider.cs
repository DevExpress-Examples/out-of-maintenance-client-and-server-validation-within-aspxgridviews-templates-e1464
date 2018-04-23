using System;
using System.Collections.Generic;
using System.Web;
using System.Web.SessionState;

namespace GridViewCustomValidationInTemplates {

    public static class DataProvider {
        private const string SessionKey = "MySampleData";

        private static HttpSessionState Session {
            get { return HttpContext.Current.Session; }
        }

        public static IList<Record> GetData() {
            IList<Record> data = Session[SessionKey] as IList<Record>;
            if(data == null) {
                data = CreateData();
                Session[SessionKey] = data;
            }
            return data;
        }
        public static Record FindRecordByID(int id) {
            IList<Record> data = GetData();
            foreach(Record record in data)
                if(record.ID == id)
                    return record;
            return null;
        }

        private static IList<Record> CreateData() {
            return new List<Record>(new Record[] {
                /* new Record(id, value = null, min = null, max = null) */
                new Record(1),
                new Record(2, null),
                new Record(3, 1),
                new Record(4, 2, 1, 3),
                new Record(5, 0, 1, 3),
                new Record(6, 4, 1, 3),
                new Record(7, 4, 1, null),
                new Record(8, 4, 1, null),
                new Record(9, null, 1, 2),
                new Record(10, -2, -1, 1)
            });
        }
    }

}