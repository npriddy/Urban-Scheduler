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
using UrbanSchedulerProject.Entities;

namespace Urban.Data
{
    /// <summary>
    /// The class representing the dbo.RoomReservation table.
    /// </summary>
    [System.Data.Linq.Mapping.Table(Name="dbo.RoomReservation")]
    [System.Runtime.Serialization.DataContract(IsReference = true)]
    [System.ComponentModel.DataAnnotations.ScaffoldTable(true)]
    [System.ComponentModel.DataAnnotations.MetadataType(typeof(Urban.Data.RoomReservation.Metadata))]
    [System.Data.Services.Common.DataServiceKey("Id")]
    [System.Diagnostics.DebuggerDisplay("Id: {Id}")]
    public partial class RoomReservation
        : LinqEntityBase, ICloneable 
    {
        #region Static Constructor
        /// <summary>
        /// Initializes the <see cref="RoomReservation"/> class.
        /// </summary>
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        static RoomReservation()
        {
            AddSharedRules();
        }
        #endregion

        #region Default Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="RoomReservation"/> class.
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCode]
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        public RoomReservation()
        {
            Initialize();
        }

        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        private void Initialize()
        {
            _room = default(System.Data.Linq.EntityRef<Room>);
            _reserverUser = default(System.Data.Linq.EntityRef<User>);
            _roomReservationCommentsList = new System.Data.Linq.EntitySet<RoomReservationComments>(OnRoomReservationCommentsListAdd, OnRoomReservationCommentsListRemove);
            _roomReservationDatesList = new System.Data.Linq.EntitySet<RoomReservationDates>(OnRoomReservationDatesListAdd, OnRoomReservationDatesListRemove);
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
        private int _roomID;

        /// <summary>
        /// Gets or sets the RoomID column value.
        /// </summary>
        [System.Data.Linq.Mapping.Column(Name = "RoomID", Storage = "_roomID", DbType = "int NOT NULL", CanBeNull = false)]
        [System.Runtime.Serialization.DataMember(Order = 2)]
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        public int RoomID
        {
            get { return _roomID; }
            set
            {
                if (_roomID != value)
                {
                    if (_room.HasLoadedOrAssignedValue)
                    {
                        throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
                    }
                    OnRoomIDChanging(value);
                    SendPropertyChanging("RoomID");
                    _roomID = value;
                    SendPropertyChanged("RoomID");
                    OnRoomIDChanged();
                }
            }
        }

        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        private int _reserverUserID;

        /// <summary>
        /// Gets or sets the ReserverUserID column value.
        /// </summary>
        [System.Data.Linq.Mapping.Column(Name = "ReserverUserID", Storage = "_reserverUserID", DbType = "int NOT NULL", CanBeNull = false)]
        [System.Runtime.Serialization.DataMember(Order = 3)]
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        public int ReserverUserID
        {
            get { return _reserverUserID; }
            set
            {
                if (_reserverUserID != value)
                {
                    if (_reserverUser.HasLoadedOrAssignedValue)
                    {
                        throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
                    }
                    OnReserverUserIDChanging(value);
                    SendPropertyChanging("ReserverUserID");
                    _reserverUserID = value;
                    SendPropertyChanged("ReserverUserID");
                    OnReserverUserIDChanged();
                }
            }
        }

        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        private Nullable<bool> _approved;

        /// <summary>
        /// Gets or sets the Approved column value.
        /// </summary>
        [System.Data.Linq.Mapping.Column(Name = "Approved", Storage = "_approved", DbType = "bit")]
        [System.Runtime.Serialization.DataMember(Order = 4)]
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

        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        private Nullable<System.DateTime> _requestedDate;

        /// <summary>
        /// Gets or sets the RequestedDate column value.
        /// </summary>
        [System.Data.Linq.Mapping.Column(Name = "RequestedDate", Storage = "_requestedDate", DbType = "datetime")]
        [System.Runtime.Serialization.DataMember(Order = 5)]
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        public Nullable<System.DateTime> RequestedDate
        {
            get { return _requestedDate; }
            set
            {
                if (_requestedDate != value)
                {
                    OnRequestedDateChanging(value);
                    SendPropertyChanging("RequestedDate");
                    _requestedDate = value;
                    SendPropertyChanged("RequestedDate");
                    OnRequestedDateChanged();
                }
            }
        }
        #endregion

        #region Association Mapped Properties

        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        private System.Data.Linq.EntityRef<Room> _room;

        /// <summary>
        /// Gets or sets the <see cref="Room"/> association.
        /// </summary>
        [System.Data.Linq.Mapping.Association(Name = "Room_RoomReservation", Storage = "_room", ThisKey = "RoomID", OtherKey = "Id", IsForeignKey = true)]
        [System.Runtime.Serialization.DataMember(Order = 6, EmitDefaultValue = false)]
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        public Room Room
        {
            get { return (serializing && !_room.HasLoadedOrAssignedValue) ? null : _room.Entity; }
            set
            {
                Room previousValue = _room.Entity;
                if (previousValue != value || _room.HasLoadedOrAssignedValue == false)
                {
                    OnRoomChanging(value);
                    SendPropertyChanging("Room");
                    if (previousValue != null)
                    {
                        _room.Entity = null;
                        previousValue.RoomReservationList.Remove(this);
                    }
                    _room.Entity = value;
                    if (value != null)
                    {
                        value.RoomReservationList.Add(this);
                        _roomID = value.Id;
                    }
                    else
                    {
                        _roomID = default(int);
                    }
                    SendPropertyChanged("Room");
                    OnRoomChanged();
                }
            }
        }
        
        
        

        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        private System.Data.Linq.EntityRef<User> _reserverUser;

        /// <summary>
        /// Gets or sets the <see cref="User"/> association.
        /// </summary>
        [System.Data.Linq.Mapping.Association(Name = "User_RoomReservation", Storage = "_reserverUser", ThisKey = "ReserverUserID", OtherKey = "Id", IsForeignKey = true)]
        [System.Runtime.Serialization.DataMember(Order = 7, EmitDefaultValue = false)]
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        public User ReserverUser
        {
            get { return (serializing && !_reserverUser.HasLoadedOrAssignedValue) ? null : _reserverUser.Entity; }
            set
            {
                User previousValue = _reserverUser.Entity;
                if (previousValue != value || _reserverUser.HasLoadedOrAssignedValue == false)
                {
                    OnReserverUserChanging(value);
                    SendPropertyChanging("ReserverUser");
                    if (previousValue != null)
                    {
                        _reserverUser.Entity = null;
                        previousValue.ReserverRoomReservationList.Remove(this);
                    }
                    _reserverUser.Entity = value;
                    if (value != null)
                    {
                        value.ReserverRoomReservationList.Add(this);
                        _reserverUserID = value.Id;
                    }
                    else
                    {
                        _reserverUserID = default(int);
                    }
                    SendPropertyChanged("ReserverUser");
                    OnReserverUserChanged();
                }
            }
        }
        
        
        

        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        private System.Data.Linq.EntitySet<RoomReservationComments> _roomReservationCommentsList;

        /// <summary>
        /// Gets or sets the <see cref="RoomReservationComments"/> association.
        /// </summary>
        [System.Data.Linq.Mapping.Association(Name = "RoomReservation_RoomReservationComments", Storage = "_roomReservationCommentsList", ThisKey = "Id", OtherKey = "RoomReservationID")]
        [System.Runtime.Serialization.DataMember(Order=8, EmitDefaultValue=false)]
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        public System.Data.Linq.EntitySet<RoomReservationComments> RoomReservationCommentsList
        {
            get { return (serializing && !_roomReservationCommentsList.HasLoadedOrAssignedValues) ? null : _roomReservationCommentsList; }
            set { _roomReservationCommentsList.Assign(value); }
        }
        
        

        [System.Diagnostics.DebuggerNonUserCode]
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        private void OnRoomReservationCommentsListAdd(RoomReservationComments entity)
        {
            SendPropertyChanging(null);
            entity.RoomReservation = this;
            SendPropertyChanged(null);
        }

        [System.Diagnostics.DebuggerNonUserCode]
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        private void OnRoomReservationCommentsListRemove(RoomReservationComments entity)
        {
            SendPropertyChanging(null);
            entity.RoomReservation = null;
            SendPropertyChanged(null);
        }

        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        private System.Data.Linq.EntitySet<RoomReservationDates> _roomReservationDatesList;

        /// <summary>
        /// Gets or sets the <see cref="RoomReservationDates"/> association.
        /// </summary>
        [System.Data.Linq.Mapping.Association(Name = "RoomReservation_RoomReservationDates", Storage = "_roomReservationDatesList", ThisKey = "Id", OtherKey = "RoomReservationID")]
        [System.Runtime.Serialization.DataMember(Order=9, EmitDefaultValue=false)]
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        public System.Data.Linq.EntitySet<RoomReservationDates> RoomReservationDatesList
        {
            get { return (serializing && !_roomReservationDatesList.HasLoadedOrAssignedValues) ? null : _roomReservationDatesList; }
            set { _roomReservationDatesList.Assign(value); }
        }
        
        

        [System.Diagnostics.DebuggerNonUserCode]
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        private void OnRoomReservationDatesListAdd(RoomReservationDates entity)
        {
            SendPropertyChanging(null);
            entity.RoomReservation = this;
            SendPropertyChanged(null);
        }

        [System.Diagnostics.DebuggerNonUserCode]
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        private void OnRoomReservationDatesListRemove(RoomReservationDates entity)
        {
            SendPropertyChanging(null);
            entity.RoomReservation = null;
            SendPropertyChanged(null);
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
        /// <summary>Called when <see cref="RoomID"/> is changing.</summary>
        /// <param name="value">The new value.</param>
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        partial void OnRoomIDChanging(int value);
        /// <summary>Called after <see cref="RoomID"/> has Changed.</summary>
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        partial void OnRoomIDChanged();
        /// <summary>Called when <see cref="ReserverUserID"/> is changing.</summary>
        /// <param name="value">The new value.</param>
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        partial void OnReserverUserIDChanging(int value);
        /// <summary>Called after <see cref="ReserverUserID"/> has Changed.</summary>
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        partial void OnReserverUserIDChanged();
        /// <summary>Called when <see cref="Approved"/> is changing.</summary>
        /// <param name="value">The new value.</param>
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        partial void OnApprovedChanging(Nullable<bool> value);
        /// <summary>Called after <see cref="Approved"/> has Changed.</summary>
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        partial void OnApprovedChanged();
        /// <summary>Called when <see cref="RequestedDate"/> is changing.</summary>
        /// <param name="value">The new value.</param>
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        partial void OnRequestedDateChanging(Nullable<System.DateTime> value);
        /// <summary>Called after <see cref="RequestedDate"/> has Changed.</summary>
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        partial void OnRequestedDateChanged();
        /// <summary>Called when <see cref="Room"/> is changing.</summary>
        /// <param name="value">The new value.</param>
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        partial void OnRoomChanging(Room value);
        /// <summary>Called after <see cref="Room"/> has Changed.</summary>
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        partial void OnRoomChanged();
        /// <summary>Called when <see cref="ReserverUser"/> is changing.</summary>
        /// <param name="value">The new value.</param>
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        partial void OnReserverUserChanging(User value);
        /// <summary>Called after <see cref="ReserverUser"/> has Changed.</summary>
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        partial void OnReserverUserChanged();

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
        /// Deserializes an instance of <see cref="RoomReservation"/> from XML.
        /// </summary>
        /// <param name="xml">The XML string representing a <see cref="RoomReservation"/> instance.</param>
        /// <returns>An instance of <see cref="RoomReservation"/> that is deserialized from the XML string.</returns>
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        public static RoomReservation FromXml(string xml)
        {
            var deserializer = new System.Runtime.Serialization.DataContractSerializer(typeof(RoomReservation));

            using (var sr = new System.IO.StringReader(xml))
            using (var reader = System.Xml.XmlReader.Create(sr))
            {
                return deserializer.ReadObject(reader) as RoomReservation;
            }
        }

        /// <summary>
        /// Deserializes an instance of <see cref="RoomReservation"/> from a byte array.
        /// </summary>
        /// <param name="buffer">The byte array representing a <see cref="RoomReservation"/> instance.</param>
        /// <returns>An instance of <see cref="RoomReservation"/> that is deserialized from the byte array.</returns>
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        public static RoomReservation FromBinary(byte[] buffer)
        {
            var deserializer = new System.Runtime.Serialization.DataContractSerializer(typeof(RoomReservation));

            using (var ms = new System.IO.MemoryStream(buffer))
            using (var reader = System.Xml.XmlDictionaryReader.CreateBinaryReader(ms, System.Xml.XmlDictionaryReaderQuotas.Max))
            {
                return deserializer.ReadObject(reader) as RoomReservation;
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
        public RoomReservation Clone()
        {
            return (RoomReservation)((ICloneable)this).Clone();
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
            _room = Detach(_room);
            _reserverUser = Detach(_reserverUser);
            _roomReservationCommentsList = Detach(_roomReservationCommentsList, OnRoomReservationCommentsListAdd, OnRoomReservationCommentsListRemove);
            _roomReservationDatesList = Detach(_roomReservationDatesList, OnRoomReservationDatesListAdd, OnRoomReservationDatesListRemove);
        }
        #endregion
    }
}
#pragma warning restore 1591
#pragma warning restore 0414

