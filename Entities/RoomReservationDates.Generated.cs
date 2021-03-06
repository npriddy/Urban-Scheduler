﻿#pragma warning disable 1591
#pragma warning disable 0414        
//------------------------------------------------------------------------------
// <autogenerated>
//     This code was generated by a CodeSmith Template.
//
//     DO NOT MODIFY contents of this file. Changes to this
//     file will be lost if the code is regenerated.
// </autogenerated>
//------------------------------------------------------------------------------
using System;
using System.Linq;

namespace Urban.Data
{
    /// <summary>
    /// The class representing the dbo.RoomReservationDates table.
    /// </summary>
    [System.Data.Linq.Mapping.Table(Name="dbo.RoomReservationDates")]
    [System.Runtime.Serialization.DataContract(IsReference = true)]
    [System.ComponentModel.DataAnnotations.ScaffoldTable(true)]
    [System.ComponentModel.DataAnnotations.MetadataType(typeof(Urban.Data.RoomReservationDates.Metadata))]
    [System.Data.Services.Common.DataServiceKey("Id")]
    [System.Diagnostics.DebuggerDisplay("Id: {Id}")]
    public partial class RoomReservationDates
        : LinqEntityBase, ICloneable 
    {
        #region Static Constructor
        /// <summary>
        /// Initializes the <see cref="RoomReservationDates"/> class.
        /// </summary>
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        static RoomReservationDates()
        {
            AddSharedRules();
        }
        #endregion

        #region Default Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="RoomReservationDates"/> class.
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCode]
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        public RoomReservationDates()
        {
            Initialize();
        }

        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        private void Initialize()
        {
            _roomReservation = default(System.Data.Linq.EntityRef<RoomReservation>);
            OnCreated();
        }
        #endregion

        #region Column Mapped Properties

        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        private int _id = default(int);

        /// <summary>
        /// Gets the ID column value.
        /// </summary>
        [System.Data.Linq.Mapping.Column(Name = "ID", Storage = "_id", DbType = "int NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true, CanBeNull = false)]
        [System.Runtime.Serialization.DataMember(Order = 1)]
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        public int Id
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    OnIdChanging(value);
                    SendPropertyChanging("Id");
                    _id = value;
                    SendPropertyChanged("Id");
                    OnIdChanged();
                }
            }
        }

        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        private int _roomReservationID;

        /// <summary>
        /// Gets or sets the RoomReservationID column value.
        /// </summary>
        [System.Data.Linq.Mapping.Column(Name = "RoomReservationID", Storage = "_roomReservationID", DbType = "int NOT NULL", CanBeNull = false)]
        [System.Runtime.Serialization.DataMember(Order = 2)]
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        public int RoomReservationID
        {
            get { return _roomReservationID; }
            set
            {
                if (_roomReservationID != value)
                {
                    if (_roomReservation.HasLoadedOrAssignedValue)
                    {
                        throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
                    }
                    OnRoomReservationIDChanging(value);
                    SendPropertyChanging("RoomReservationID");
                    _roomReservationID = value;
                    SendPropertyChanged("RoomReservationID");
                    OnRoomReservationIDChanged();
                }
            }
        }

        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        private System.DateTime _startDate;

        /// <summary>
        /// Gets or sets the StartDate column value.
        /// </summary>
        [System.Data.Linq.Mapping.Column(Name = "StartDate", Storage = "_startDate", DbType = "datetime NOT NULL", CanBeNull = false)]
        [System.Runtime.Serialization.DataMember(Order = 3)]
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        public System.DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                if (_startDate != value)
                {
                    OnStartDateChanging(value);
                    SendPropertyChanging("StartDate");
                    _startDate = value;
                    SendPropertyChanged("StartDate");
                    OnStartDateChanged();
                }
            }
        }

        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        private System.DateTime _endDate;

        /// <summary>
        /// Gets or sets the EndDate column value.
        /// </summary>
        [System.Data.Linq.Mapping.Column(Name = "EndDate", Storage = "_endDate", DbType = "datetime NOT NULL", CanBeNull = false)]
        [System.Runtime.Serialization.DataMember(Order = 4)]
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        public System.DateTime EndDate
        {
            get { return _endDate; }
            set
            {
                if (_endDate != value)
                {
                    OnEndDateChanging(value);
                    SendPropertyChanging("EndDate");
                    _endDate = value;
                    SendPropertyChanged("EndDate");
                    OnEndDateChanged();
                }
            }
        }

        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        private Nullable<bool> _allDay;

        /// <summary>
        /// Gets or sets the AllDay column value.
        /// </summary>
        [System.Data.Linq.Mapping.Column(Name = "AllDay", Storage = "_allDay", DbType = "bit")]
        [System.Runtime.Serialization.DataMember(Order = 5)]
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        public Nullable<bool> AllDay
        {
            get { return _allDay; }
            set
            {
                if (_allDay != value)
                {
                    OnAllDayChanging(value);
                    SendPropertyChanging("AllDay");
                    _allDay = value;
                    SendPropertyChanged("AllDay");
                    OnAllDayChanged();
                }
            }
        }

        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        private Nullable<bool> _approved;

        /// <summary>
        /// Gets or sets the Approved column value.
        /// </summary>
        [System.Data.Linq.Mapping.Column(Name = "Approved", Storage = "_approved", DbType = "bit")]
        [System.Runtime.Serialization.DataMember(Order = 6)]
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        public Nullable<bool> Approved
        {
            get { return _approved; }
            set
            {
                if (_approved != value)
                {
                    OnApprovedChanging(value);
                    SendPropertyChanging("Approved");
                    _approved = value;
                    SendPropertyChanged("Approved");
                    OnApprovedChanged();
                }
            }
        }
        #endregion

        #region Association Mapped Properties

        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        private System.Data.Linq.EntityRef<RoomReservation> _roomReservation;

        /// <summary>
        /// Gets or sets the <see cref="RoomReservation"/> association.
        /// </summary>
        [System.Data.Linq.Mapping.Association(Name = "RoomReservation_RoomReservationDates", Storage = "_roomReservation", ThisKey = "RoomReservationID", OtherKey = "Id", IsForeignKey = true)]
        [System.Runtime.Serialization.DataMember(Order = 7, EmitDefaultValue = false)]
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        public RoomReservation RoomReservation
        {
            get { return (serializing && !_roomReservation.HasLoadedOrAssignedValue) ? null : _roomReservation.Entity; }
            set
            {
                RoomReservation previousValue = _roomReservation.Entity;
                if (previousValue != value || _roomReservation.HasLoadedOrAssignedValue == false)
                {
                    OnRoomReservationChanging(value);
                    SendPropertyChanging("RoomReservation");
                    if (previousValue != null)
                    {
                        _roomReservation.Entity = null;
                        previousValue.RoomReservationDatesList.Remove(this);
                    }
                    _roomReservation.Entity = value;
                    if (value != null)
                    {
                        value.RoomReservationDatesList.Add(this);
                        _roomReservationID = value.Id;
                    }
                    else
                    {
                        _roomReservationID = default(int);
                    }
                    SendPropertyChanged("RoomReservation");
                    OnRoomReservationChanged();
                }
            }
        }
        
        
        
        #endregion

        #region Extensibility Method Definitions
        /// <summary>Called by the static constructor to add shared rules.</summary>
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        static partial void AddSharedRules();
        /// <summary>Called when this instance is loaded.</summary>
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        partial void OnLoaded();
        /// <summary>Called when this instance is being saved.</summary>
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        /// <summary>Called when this instance is created.</summary>
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        partial void OnCreated();
        /// <summary>Called when <see cref="Id"/> is changing.</summary>
        /// <param name="value">The new value.</param>
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        partial void OnIdChanging(int value);
        /// <summary>Called after <see cref="Id"/> has Changed.</summary>
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        partial void OnIdChanged();
        /// <summary>Called when <see cref="RoomReservationID"/> is changing.</summary>
        /// <param name="value">The new value.</param>
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        partial void OnRoomReservationIDChanging(int value);
        /// <summary>Called after <see cref="RoomReservationID"/> has Changed.</summary>
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        partial void OnRoomReservationIDChanged();
        /// <summary>Called when <see cref="StartDate"/> is changing.</summary>
        /// <param name="value">The new value.</param>
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        partial void OnStartDateChanging(System.DateTime value);
        /// <summary>Called after <see cref="StartDate"/> has Changed.</summary>
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        partial void OnStartDateChanged();
        /// <summary>Called when <see cref="EndDate"/> is changing.</summary>
        /// <param name="value">The new value.</param>
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        partial void OnEndDateChanging(System.DateTime value);
        /// <summary>Called after <see cref="EndDate"/> has Changed.</summary>
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        partial void OnEndDateChanged();
        /// <summary>Called when <see cref="AllDay"/> is changing.</summary>
        /// <param name="value">The new value.</param>
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        partial void OnAllDayChanging(Nullable<bool> value);
        /// <summary>Called after <see cref="AllDay"/> has Changed.</summary>
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        partial void OnAllDayChanged();
        /// <summary>Called when <see cref="Approved"/> is changing.</summary>
        /// <param name="value">The new value.</param>
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        partial void OnApprovedChanging(Nullable<bool> value);
        /// <summary>Called after <see cref="Approved"/> has Changed.</summary>
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        partial void OnApprovedChanged();
        /// <summary>Called when <see cref="RoomReservation"/> is changing.</summary>
        /// <param name="value">The new value.</param>
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        partial void OnRoomReservationChanging(RoomReservation value);
        /// <summary>Called after <see cref="RoomReservation"/> has Changed.</summary>
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        partial void OnRoomReservationChanged();

        #endregion

        #region Serialization
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        private bool serializing;

        /// <summary>
        /// Called when serializing.
        /// </summary>
        /// <param name="context">The <see cref="System.Runtime.Serialization.StreamingContext"/> for the serialization.</param>
        [System.Runtime.Serialization.OnSerializing]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        public void OnSerializing(System.Runtime.Serialization.StreamingContext context) {
            serializing = true;
        }

        /// <summary>
        /// Called when serialized.
        /// </summary>
        /// <param name="context">The <see cref="System.Runtime.Serialization.StreamingContext"/> for the serialization.</param>
        [System.Runtime.Serialization.OnSerialized]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        public void OnSerialized(System.Runtime.Serialization.StreamingContext context) {
            serializing = false;
        }

        /// <summary>
        /// Called when deserializing.
        /// </summary>
        /// <param name="context">The <see cref="System.Runtime.Serialization.StreamingContext"/> for the serialization.</param>
        [System.Runtime.Serialization.OnDeserializing]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        public void OnDeserializing(System.Runtime.Serialization.StreamingContext context) {
            Initialize();
        }

        /// <summary>
        /// Deserializes an instance of <see cref="RoomReservationDates"/> from XML.
        /// </summary>
        /// <param name="xml">The XML string representing a <see cref="RoomReservationDates"/> instance.</param>
        /// <returns>An instance of <see cref="RoomReservationDates"/> that is deserialized from the XML string.</returns>
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        public static RoomReservationDates FromXml(string xml)
        {
            var deserializer = new System.Runtime.Serialization.DataContractSerializer(typeof(RoomReservationDates));

            using (var sr = new System.IO.StringReader(xml))
            using (var reader = System.Xml.XmlReader.Create(sr))
            {
                return deserializer.ReadObject(reader) as RoomReservationDates;
            }
        }

        /// <summary>
        /// Deserializes an instance of <see cref="RoomReservationDates"/> from a byte array.
        /// </summary>
        /// <param name="buffer">The byte array representing a <see cref="RoomReservationDates"/> instance.</param>
        /// <returns>An instance of <see cref="RoomReservationDates"/> that is deserialized from the byte array.</returns>
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        public static RoomReservationDates FromBinary(byte[] buffer)
        {
            var deserializer = new System.Runtime.Serialization.DataContractSerializer(typeof(RoomReservationDates));

            using (var ms = new System.IO.MemoryStream(buffer))
            using (var reader = System.Xml.XmlDictionaryReader.CreateBinaryReader(ms, System.Xml.XmlDictionaryReaderQuotas.Max))
            {
                return deserializer.ReadObject(reader) as RoomReservationDates;
            }
        }
        
        #endregion

        #region Clone
        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        object ICloneable.Clone()
        {
            var serializer = new System.Runtime.Serialization.DataContractSerializer(GetType());
            using (var ms = new System.IO.MemoryStream())
            {
                serializer.WriteObject(ms, this);
                ms.Position = 0;
                return serializer.ReadObject(ms);
            }
        }

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        /// <remarks>
        /// Only loaded <see cref="T:System.Data.Linq.EntityRef`1"/> and <see cref="T:System.Data.Linq.EntitySet`1" /> child accessions will be cloned.
        /// </remarks>
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        public RoomReservationDates Clone()
        {
            return (RoomReservationDates)((ICloneable)this).Clone();
        }
        #endregion

        #region Detach Methods
        /// <summary>
        /// Detach this instance from the <see cref="System.Data.Linq.DataContext"/>.
        /// </summary>
        /// <remarks>
        /// Detaching the entity will stop all lazy loading and allow it to be added to another <see cref="System.Data.Linq.DataContext"/>.
        /// </remarks>
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        public override void Detach()
        {
            if (!IsAttached())
                return;

            base.Detach();
            _roomReservation = Detach(_roomReservation);
        }
        #endregion
    }
}
#pragma warning restore 1591
#pragma warning restore 0414

