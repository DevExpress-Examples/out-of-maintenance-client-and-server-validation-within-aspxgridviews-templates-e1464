using System;

namespace GridViewCustomValidationInTemplates {

    public class Record {
        private int _id;
        private int? _min;
        private int? _max;
        private int? _value;

        public Record(int id)
            : this(id, null) {
        }
        public Record(int id, int? value)
            : this(id, value, null, null) {
        }
        public Record(int id, int? value, int? min, int? max) {
            this._id = id;
            this._value = value;
            this._min = min;
            this._max = max;
        }

        public int ID {
            get { return _id; }
        }
        public int? Min {
            get { return _min; }
        }
        public int? Max {
            get { return _max; }
        }
        public int? Value {
            get { return _value; }
            set { _value = value; }
        }
    }

}