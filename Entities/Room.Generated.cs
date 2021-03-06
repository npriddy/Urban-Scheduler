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
    /// The class representing the dbo.Room table.
    /// </summary>
    [System.Data.Linq.Mapping.Table(Name="dbo.Room")]
    [System.Runtime.Serialization.DataContract(IsReference = true)]
    [System.ComponentModel.DataAnnotations.ScaffoldTable(true)]
    [System.ComponentModel.DataAnnotations.MetadataType(typeof(Urban.Data.Room.Metadata))]
    [System.Data.Services.Common.DataServiceKey("Id")]
    [System.Diagnostics.DebuggerDisplay("Id: {Id}")]
    public partial class Room
        : LinqEntityBase, ICloneable 
    {
        #region Static Constructor
        /// <summary>
        /// Initializes the <see cref="Room"/> class.
        /// </summary>
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        static Room()
        {
            AddSharedRules();
        }
        #endregion

        #region Default Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Room"/> class.
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCode]
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        public Room()
        {
            Initialize();
        }

        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        private void Initialize()
        {
            _building = default(System.Data.Linq.EntityRef<Building>);
            _roomType = default(System.Data.Linq.EntityRef<RoomType>);
            _user = default(System.Data.Linq.EntityRef<User>);
            _roomAvailabilityList = new System.Data.Linq.EntitySet<RoomAvailability>(OnRoomAvailabilityListAdd, OnRoomAvailabilityListRemove);
            _roomCommentsList = new System.Data.Linq.EntitySet<RoomComments>(OnRoomCommentsListAdd, OnRoomCommentsListRemove);
            _roomImageLinkList = new System.Data.Linq.EntitySet<RoomImageLink>(OnRoomImageLinkListAdd, OnRoomImageLinkListRemove);
            _roomReservationList = new System.Data.Linq.EntitySet<RoomReservation>(OnRoomReservationListAdd, OnRoomReservationListRemove);
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
                    if (_user.HasLoadedOrAssignedValue)
                    {
                        throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
                    }
                    OnIdChanging(value);
                    SendPropertyChanging("Id");
                    _id = value;
                    SendPropertyChanged("Id");
                    OnIdChanged();
                }
            }
        }

        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        private int _userID;

        /// <summary>
        /// Gets or sets the UserID column value.
        /// </summary>
        [System.Data.Linq.Mapping.Column(Name = "UserID", Storage = "_userID", DbType = "int NOT NULL", CanBeNull = false)]
        [System.Runtime.Serialization.DataMember(Order = 2)]
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        public int UserID
        {
            get { return _userID; }
            set
            {
                if (_userID != value)
                {
                    OnUserIDChanging(value);
                    SendPropertyChanging("UserID");
                    _userID = value;
                    SendPropertyChanged("UserID");
                    OnUserIDChanged();
                }
            }
        }

        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        private Nullable<int> _buildingID;

        /// <summary>
        /// Gets or sets the BuildingID column value.
        /// </summary>
        [System.Data.Linq.Mapping.Column(Name = "BuildingID", Storage = "_buildingID", DbType = "int")]
        [System.Runtime.Serialization.DataMember(Order = 3)]
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        public Nullable<int> BuildingID
        {
            get { return _buildingID; }
            set
            {
                if (_buildingID != value)
                {
                    if (_building.HasLoadedOrAssignedValue)
                    {
                        throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
                    }
                    OnBuildingIDChanging(value);
                    SendPropertyChanging("BuildingID");
                    _buildingID = value;
                    SendPropertyChanged("BuildingID");
                    OnBuildingIDChanged();
                }
            }
        }

        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        private int _roomTypeID;

        /// <summary>
        /// Gets or sets the RoomTypeID column value.
        /// </summary>
        [System.Data.Linq.Mapping.Column(Name = "RoomTypeID", Storage = "_roomTypeID", DbType = "int NOT NULL", CanBeNull = false)]
        [System.Runtime.Serialization.DataMember(Order = 4)]
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        public int RoomTypeID
        {
            get { return _roomTypeID; }
            set
            {
                if (_roomTypeID != value)
                {
                    if (_roomType.HasLoadedOrAssignedValue)
                    {
                        throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
                    }
                    OnRoomTypeIDChanging(value);
                    SendPropertyChanging("RoomTypeID");
                    _roomTypeID = value;
                    SendPropertyChanged("RoomTypeID");
                    OnRoomTypeIDChanged();
                }
            }
        }

        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        private string _number;

        /// <summary>
        /// Gets or sets the Number column value.
        /// </summary>
        [System.Data.Linq.Mapping.Column(Name = "Number", Storage = "_number", DbType = "nvarchar(50) NOT NULL", CanBeNull = false)]
        [System.ComponentModel.DataAnnotations.StringLength(50)]
        [System.Runtime.Serialization.DataMember(Order = 5)]
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        public string Number
        {
            get { return _number; }
            set
            {
                if (_number != value)
                {
                    OnNumberChanging(value);
                    SendPropertyChanging("Number");
                    _number = value;
                    SendPropertyChanged("Number");
                    OnNumberChanged();
                }
            }
        }

        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        private int _maxOccupancy;

        /// <summary>
        /// Gets or sets the MaxOccupancy column value.
        /// </summary>
        [System.Data.Linq.Mapping.Column(Name = "MaxOccupancy", Storage = "_maxOccupancy", DbType = "int NOT NULL", CanBeNull = false)]
        [System.Runtime.Serialization.DataMember(Order = 6)]
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        public int MaxOccupancy
        {
            get { return _maxOccupancy; }
            set
            {
                if (_maxOccupancy != value)
                {
                    OnMaxOccupancyChanging(value);
                    SendPropertyChanging("MaxOccupancy");
                    _maxOccupancy = value;
                    SendPropertyChanged("MaxOccupancy");
                    OnMaxOccupancyChanged();
                }
            }
        }

        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        private string _title;

        /// <summary>
        /// Gets or sets the Title column value.
        /// </summary>
        [System.Data.Linq.Mapping.Column(Name = "Title", Storage = "_title", DbType = "nvarchar(100) NOT NULL", CanBeNull = false)]
        [System.ComponentModel.DataAnnotations.StringLength(100)]
        [System.Runtime.Serialization.DataMember(Order = 7)]
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        public string Title
        {
            get { return _title; }
            set
            {
                if (_title != value)
                {
                    OnTitleChanging(value);
                    SendPropertyChanging("Title");
                    _title = value;
                    SendPropertyChanged("Title");
                    OnTitleChanged();
                }
            }
        }

        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        private string _description;

        /// <summary>
        /// Gets or sets the Description column value.
        /// </summary>
        [System.Data.Linq.Mapping.Column(Name = "Description", Storage = "_description", DbType = "nvarchar(MAX)")]
        [System.Runtime.Serialization.DataMember(Order = 8)]
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        public string Description
        {
            get { return _description; }
            set
            {
                if (_description != value)
                {
                    OnDescriptionChanging(value);
                    SendPropertyChanging("Description");
                    _description = value;
                    SendPropertyChanged("Description");
                    OnDescriptionChanged();
                }
            }
        }
        #endregion

        #region Association Mapped Properties

        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        private System.Data.Linq.EntityRef<Building> _building;

        /// <summary>
        /// Gets or sets the <see cref="Building"/> association.
        /// </summary>
        [System.Data.Linq.Mapping.Association(Name = "Building_Room", Storage = "_building", ThisKey = "BuildingID", OtherKey = "Id", IsForeignKey = true)]
        [System.Runtime.Serialization.DataMember(Order = 9, EmitDefaultValue = false)]
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        public Building Building
        {
            get { return (serializing && !_building.HasLoadedOrAssignedValue) ? null : _building.Entity; }
            set
            {
                Building previousValue = _building.Entity;
                if (previousValue != value || _building.HasLoadedOrAssignedValue == false)
                {
                    OnBuildingChanging(value);
                    SendPropertyChanging("Building");
                    if (previousValue != null)
                    {
                        _building.Entity = null;
                        previousValue.RoomList.Remove(this);
                    }
                    _building.Entity = value;
                    if (value != null)
                    {
                        value.RoomList.Add(this);
                        _buildingID = value.Id;
                    }
                    else
                    {
                        _buildingID = default(Nullable<int>);
                    }
                    SendPropertyChanged("Building");
                    OnBuildingChanged();
                }
            }
        }
        
        
        

        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        private System.Data.Linq.EntityRef<RoomType> _roomType;

        /// <summary>
        /// Gets or sets the <see cref="RoomType"/> association.
        /// </summary>
        [System.Data.Linq.Mapping.Association(Name = "RoomType_Room", Storage = "_roomType", ThisKey = "RoomTypeID", OtherKey = "Id", IsForeignKey = true)]
        [System.Runtime.Serialization.DataMember(Order = 10, EmitDefaultValue = false)]
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        public RoomType RoomType
        {
            get { return (serializing && !_roomType.HasLoadedOrAssignedValue) ? null : _roomType.Entity; }
            set
            {
                RoomType previousValue = _roomType.Entity;
                if (previousValue != value || _roomType.HasLoadedOrAssignedValue == false)
                {
                    OnRoomTypeChanging(value);
                    SendPropertyChanging("RoomType");
                    if (previousValue != null)
                    {
                        _roomType.Entity = null;
                        previousValue.RoomList.Remove(this);
                    }
                    _roomType.Entity = value;
                    if (value != null)
                    {
                        value.RoomList.Add(this);
                        _roomTypeID = value.Id;
                    }
                    else
                    {
                        _roomTypeID = default(int);
                    }
                    SendPropertyChanged("RoomType");
                    OnRoomTypeChanged();
                }
            }
        }
        
        
        

        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        private System.Data.Linq.EntityRef<User> _user;

        /// <summary>
        /// Gets or sets the <see cref="User"/> association.
        /// </summary>
        [System.Data.Linq.Mapping.Association(Name = "User_Room", Storage = "_user", ThisKey = "Id", OtherKey = "Id", IsForeignKey = true)]
        [System.Runtime.Serialization.DataMember(Order = 11, EmitDefaultValue = false)]
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        public User User
        {
            get { return (serializing && !_user.HasLoadedOrAssignedValue) ? null : _user.Entity; }
            set
            {
                User previousValue = _user.Entity;
                if (previousValue != value || _user.HasLoadedOrAssignedValue == false)
                {
                    OnUserChanging(value);
                    SendPropertyChanging("User");
                    if (previousValue != null)
                    {
                        _user.Entity = null;
                        previousValue.Room = null;
                    }
                    _user.Entity = value;
                    if (value != null)
                    {
                        value.Room = this;
                        _id = value.Id;
                    }
                    else
                    {
                        _id = default(int);
                    }
                    SendPropertyChanged("User");
                    OnUserChanged();
                }
            }
        }
        
        
        

        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        private System.Data.Linq.EntitySet<RoomAvailability> _roomAvailabilityList;

        /// <summary>
        /// Gets or sets the <see cref="RoomAvailability"/> association.
        /// </summary>
        [System.Data.Linq.Mapping.Association(Name = "Room_RoomAvailability", Storage = "_roomAvailabilityList", ThisKey = "Id", OtherKey = "RoomID")]
        [System.Runtime.Serialization.DataMember(Order=12, EmitDefaultValue=false)]
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        public System.Data.Linq.EntitySet<RoomAvailability> RoomAvailabilityList
        {
            get { return (serializing && !_roomAvailabilityList.HasLoadedOrAssignedValues) ? null : _roomAvailabilityList; }
            set { _roomAvailabilityList.Assign(value); }
        }
        
        

        [System.Diagnostics.DebuggerNonUserCode]
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        private void OnRoomAvailabilityListAdd(RoomAvailability entity)
        {
            SendPropertyChanging(null);
            entity.Room = this;
            SendPropertyChanged(null);
        }

        [System.Diagnostics.DebuggerNonUserCode]
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        private void OnRoomAvailabilityListRemove(RoomAvailability entity)
        {
            SendPropertyChanging(null);
            entity.Room = null;
            SendPropertyChanged(null);
        }

        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        private System.Data.Linq.EntitySet<RoomComments> _roomCommentsList;

        /// <summary>
        /// Gets or sets the <see cref="RoomComments"/> association.
        /// </summary>
        [System.Data.Linq.Mapping.Association(Name = "Room_RoomComments", Storage = "_roomCommentsList", ThisKey = "Id", OtherKey = "RoomID")]
        [System.Runtime.Serialization.DataMember(Order=13, EmitDefaultValue=false)]
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        public System.Data.Linq.EntitySet<RoomComments> RoomCommentsList
        {
            get { return (serializing && !_roomCommentsList.HasLoadedOrAssignedValues) ? null : _roomCommentsList; }
            set { _roomCommentsList.Assign(value); }
        }
        
        

        [System.Diagnostics.DebuggerNonUserCode]
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        private void OnRoomCommentsListAdd(RoomComments entity)
        {
            SendPropertyChanging(null);
            entity.Room = this;
            SendPropertyChanged(null);
        }

        [System.Diagnostics.DebuggerNonUserCode]
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        private void OnRoomCommentsListRemove(RoomComments entity)
        {
            SendPropertyChanging(null);
            entity.Room = null;
            SendPropertyChanged(null);
        }

        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        private System.Data.Linq.EntitySet<RoomImageLink> _roomImageLinkList;

        /// <summary>
        /// Gets or sets the <see cref="RoomImageLink"/> association.
        /// </summary>
        [System.Data.Linq.Mapping.Association(Name = "Room_RoomImageLink", Storage = "_roomImageLinkList", ThisKey = "Id", OtherKey = "RoomID")]
        [System.Runtime.Serialization.DataMember(Order=14, EmitDefaultValue=false)]
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        public System.Data.Linq.EntitySet<RoomImageLink> RoomImageLinkList
        {
            get { return (serializing && !_roomImageLinkList.HasLoadedOrAssignedValues) ? null : _roomImageLinkList; }
            set { _roomImageLinkList.Assign(value); }
        }
        
        

        [System.Diagnostics.DebuggerNonUserCode]
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        private void OnRoomImageLinkListAdd(RoomImageLink entity)
        {
            SendPropertyChanging(null);
            entity.Room = this;
            SendPropertyChanged(null);
        }

        [System.Diagnostics.DebuggerNonUserCode]
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        private void OnRoomImageLinkListRemove(RoomImageLink entity)
        {
            SendPropertyChanging(null);
            entity.Room = null;
            SendPropertyChanged(null);
        }

        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        private System.Data.Linq.EntitySet<RoomReservation> _roomReservationList;

        /// <summary>
        /// Gets or sets the <see cref="RoomReservation"/> association.
        /// </summary>
        [System.Data.Linq.Mapping.Association(Name = "Room_RoomReservation", Storage = "_roomReservationList", ThisKey = "Id", OtherKey = "RoomID")]
        [System.Runtime.Serialization.DataMember(Order=15, EmitDefaultValue=false)]
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        public System.Data.Linq.EntitySet<RoomReservation> RoomReservationList
        {
            get { return (serializing && !_roomReservationList.HasLoadedOrAssignedValues) ? null : _roomReservationList; }
            set { _roomReservationList.Assign(value); }
        }
        
        

        [System.Diagnostics.DebuggerNonUserCode]
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        private void OnRoomReservationListAdd(RoomReservation entity)
        {
            SendPropertyChanging(null);
            entity.Room = this;
            SendPropertyChanged(null);
        }

        [System.Diagnostics.DebuggerNonUserCode]
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        private void OnRoomReservationListRemove(RoomReservation entity)
        {
            SendPropertyChanging(null);
            entity.Room = null;
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
        /// <summary>Called when <see cref="UserID"/> is changing.</summary>
        /// <param name="value">The new value.</param>
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        partial void OnUserIDChanging(int value);
        /// <summary>Called after <see cref="UserID"/> has Changed.</summary>
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        partial void OnUserIDChanged();
        /// <summary>Called when <see cref="BuildingID"/> is changing.</summary>
        /// <param name="value">The new value.</param>
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        partial void OnBuildingIDChanging(Nullable<int> value);
        /// <summary>Called after <see cref="BuildingID"/> has Changed.</summary>
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        partial void OnBuildingIDChanged();
        /// <summary>Called when <see cref="RoomTypeID"/> is changing.</summary>
        /// <param name="value">The new value.</param>
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        partial void OnRoomTypeIDChanging(int value);
        /// <summary>Called after <see cref="RoomTypeID"/> has Changed.</summary>
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        partial void OnRoomTypeIDChanged();
        /// <summary>Called when <see cref="Number"/> is changing.</summary>
        /// <param name="value">The new value.</param>
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        partial void OnNumberChanging(string value);
        /// <summary>Called after <see cref="Number"/> has Changed.</summary>
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        partial void OnNumberChanged();
        /// <summary>Called when <see cref="MaxOccupancy"/> is changing.</summary>
        /// <param name="value">The new value.</param>
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        partial void OnMaxOccupancyChanging(int value);
        /// <summary>Called after <see cref="MaxOccupancy"/> has Changed.</summary>
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        partial void OnMaxOccupancyChanged();
        /// <summary>Called when <see cref="Title"/> is changing.</summary>
        /// <param name="value">The new value.</param>
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        partial void OnTitleChanging(string value);
        /// <summary>Called after <see cref="Title"/> has Changed.</summary>
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        partial void OnTitleChanged();
        /// <summary>Called when <see cref="Description"/> is changing.</summary>
        /// <param name="value">The new value.</param>
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        partial void OnDescriptionChanging(string value);
        /// <summary>Called after <see cref="Description"/> has Changed.</summary>
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        partial void OnDescriptionChanged();
        /// <summary>Called when <see cref="Building"/> is changing.</summary>
        /// <param name="value">The new value.</param>
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        partial void OnBuildingChanging(Building value);
        /// <summary>Called after <see cref="Building"/> has Changed.</summary>
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        partial void OnBuildingChanged();
        /// <summary>Called when <see cref="RoomType"/> is changing.</summary>
        /// <param name="value">The new value.</param>
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        partial void OnRoomTypeChanging(RoomType value);
        /// <summary>Called after <see cref="RoomType"/> has Changed.</summary>
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        partial void OnRoomTypeChanged();
        /// <summary>Called when <see cref="User"/> is changing.</summary>
        /// <param name="value">The new value.</param>
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        partial void OnUserChanging(User value);
        /// <summary>Called after <see cref="User"/> has Changed.</summary>
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        partial void OnUserChanged();

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
        /// Deserializes an instance of <see cref="Room"/> from XML.
        /// </summary>
        /// <param name="xml">The XML string representing a <see cref="Room"/> instance.</param>
        /// <returns>An instance of <see cref="Room"/> that is deserialized from the XML string.</returns>
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        public static Room FromXml(string xml)
        {
            var deserializer = new System.Runtime.Serialization.DataContractSerializer(typeof(Room));

            using (var sr = new System.IO.StringReader(xml))
            using (var reader = System.Xml.XmlReader.Create(sr))
            {
                return deserializer.ReadObject(reader) as Room;
            }
        }

        /// <summary>
        /// Deserializes an instance of <see cref="Room"/> from a byte array.
        /// </summary>
        /// <param name="buffer">The byte array representing a <see cref="Room"/> instance.</param>
        /// <returns>An instance of <see cref="Room"/> that is deserialized from the byte array.</returns>
        [System.CodeDom.Compiler.GeneratedCode("CodeSmith", "5.0.0.0")]
        public static Room FromBinary(byte[] buffer)
        {
            var deserializer = new System.Runtime.Serialization.DataContractSerializer(typeof(Room));

            using (var ms = new System.IO.MemoryStream(buffer))
            using (var reader = System.Xml.XmlDictionaryReader.CreateBinaryReader(ms, System.Xml.XmlDictionaryReaderQuotas.Max))
            {
                return deserializer.ReadObject(reader) as Room;
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
        public Room Clone()
        {
            return (Room)((ICloneable)this).Clone();
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
            _building = Detach(_building);
            _roomType = Detach(_roomType);
            _user = Detach(_user);
            _roomAvailabilityList = Detach(_roomAvailabilityList, OnRoomAvailabilityListAdd, OnRoomAvailabilityListRemove);
            _roomCommentsList = Detach(_roomCommentsList, OnRoomCommentsListAdd, OnRoomCommentsListRemove);
            _roomImageLinkList = Detach(_roomImageLinkList, OnRoomImageLinkListAdd, OnRoomImageLinkListRemove);
            _roomReservationList = Detach(_roomReservationList, OnRoomReservationListAdd, OnRoomReservationListRemove);
        }
        #endregion
    }
}
#pragma warning restore 1591
#pragma warning restore 0414

