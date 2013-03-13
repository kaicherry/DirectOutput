﻿using System;
using System.ComponentModel;
using System.Xml.Serialization;
using DirectOutput.FX;

namespace DirectOutput.Table
{

    /// <summary>
    /// Represents a element (e.g. Switch, Solenoid) of a pinball table
    /// </summary>
    public class TableElement : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Member

        /// <summary>
        /// Is fired if the value of a property changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string PropertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
            }
        }

        #endregion



        #region  TableElementType
        private TableElementTypeEnum _TableElementType = TableElementTypeEnum.Lamp;
        /// <summary>
        /// Type of the TableElement.
        /// </summary>
        public TableElementTypeEnum TableElementType
        {
            get { return _TableElementType; }
            set
            {
                if (_TableElementType != value)
                {
                    _TableElementType = value;
                    NotifyPropertyChanged("TableElementType");
                }
            }
        }
        #endregion

        #region  Number
        private int _Number = 0;
        /// <summary>
        /// Number of the TableElement.
        /// </summary>
        public int Number
        {
            get { return _Number; }
            set
            {
                if (_Number != value)
                {

                    _Number = value;
                    NotifyPropertyChanged("Number");
                }
            }
        }

        #endregion

        #region  Name
        private string _Name = "";
        /// <summary>
        /// Name of the TableElement.<br/>
        /// Triggers NameChanged if value is changed.
        /// </summary>    
        public string Name
        {
            get { return _Name; }
            set
            {
                if (_Name != value)
                {
                    _Name = value;
                    if (NameChanged != null)
                    {
                        NameChanged(this, new EventArgs());
                        NotifyPropertyChanged("Name");
                    }
                }
            }
        }
        /// <summary>
        /// Event is fired if the value of the property Name is changed.
        /// </summary>
        public event EventHandler<EventArgs> NameChanged;
        #endregion

        #region  Value
        private int _Value = -1;
        /// <summary>
        /// Value of the TableElement.<br/>
        /// Triggers ValueChanged if the value is changed.
        /// </summary>    
        [XmlIgnoreAttribute]
        public int Value
        {
            get { return _Value; }
            set
            {
                if (_Value != value)
                {
                    _Value = value;
                    if (ValueChanged != null)
                    {
                        ValueChanged(this, new TableElementValueChangedEventArgs(this));
                        NotifyPropertyChanged("ValueChanged");
                    }
                }
            }
        }
        /// <summary>
        /// Event is fired if the value of the property State is changed.
        /// </summary>
        public event EventHandler<TableElementValueChangedEventArgs> ValueChanged;


        void TableElement_ValueChanged(object sender, TableElementValueChangedEventArgs e)
        {

            AssignedEffects.Trigger(e.TableElement);
        }
        #endregion


        private AssignedEffectList _TableElementEffectList;


        /// <summary>
        /// List of effects which are assigned to the table element. 
        /// </summary>
        public AssignedEffectList AssignedEffects
        {
            get { return _TableElementEffectList; }
            set
            {
                if (_TableElementEffectList != value)
                {
                    _TableElementEffectList = value;
                    NotifyPropertyChanged("AssignedEffects");
                }
            }
        }



        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="TableElement"/> class.
        /// </summary>
        public TableElement()
        {
            AssignedEffects = new AssignedEffectList();
            ValueChanged += new EventHandler<TableElementValueChangedEventArgs>(TableElement_ValueChanged);
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="TableElement"/> class with the specified parameters.
        /// </summary>
        /// <param name="TableElementType">Type of the TableElement.</param>
        /// <param name="Number">The number of the TableElement.</param>
        /// <param name="Value">The value of the TableElement.</param>
        public TableElement(TableElementTypeEnum TableElementType, int Number, int Value)
            : this()
        {
            this.TableElementType = TableElementType;
            this.Number = Number;
            this.Value = Value;

        }
        #endregion

    }
}
