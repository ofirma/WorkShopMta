﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Data.EntityClient;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;

[assembly: EdmSchemaAttribute()]
#region EDM Relationship Metadata

[assembly: EdmRelationshipAttribute("PicPointDBModel", "FK_Photos_Users", "Users", System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof(PicPoint.Models.Users), "Photos", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(PicPoint.Models.Photos), true)]
[assembly: EdmRelationshipAttribute("PicPointDBModel", "FK_Trips_Users", "Users", System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof(PicPoint.Models.Users), "Trips", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(PicPoint.Models.Trips), true)]

#endregion

namespace PicPoint.Models
{
    #region Contexts
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    public partial class Database1Entities1 : ObjectContext
    {
        #region Constructors
    
        /// <summary>
        /// Initializes a new Database1Entities1 object using the connection string found in the 'Database1Entities1' section of the application configuration file.
        /// </summary>
        public Database1Entities1() : base("name=Database1Entities1", "Database1Entities1")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new Database1Entities1 object.
        /// </summary>
        public Database1Entities1(string connectionString) : base(connectionString, "Database1Entities1")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new Database1Entities1 object.
        /// </summary>
        public Database1Entities1(EntityConnection connection) : base(connection, "Database1Entities1")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        #endregion
    
        #region Partial Methods
    
        partial void OnContextCreated();
    
        #endregion
    
        #region ObjectSet Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<Locations> Locations
        {
            get
            {
                if ((_Locations == null))
                {
                    _Locations = base.CreateObjectSet<Locations>("Locations");
                }
                return _Locations;
            }
        }
        private ObjectSet<Locations> _Locations;
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<M2MLocationsPhotos> M2MLocationsPhotos
        {
            get
            {
                if ((_M2MLocationsPhotos == null))
                {
                    _M2MLocationsPhotos = base.CreateObjectSet<M2MLocationsPhotos>("M2MLocationsPhotos");
                }
                return _M2MLocationsPhotos;
            }
        }
        private ObjectSet<M2MLocationsPhotos> _M2MLocationsPhotos;
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<Photos> Photos
        {
            get
            {
                if ((_Photos == null))
                {
                    _Photos = base.CreateObjectSet<Photos>("Photos");
                }
                return _Photos;
            }
        }
        private ObjectSet<Photos> _Photos;
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<Trips> Trips
        {
            get
            {
                if ((_Trips == null))
                {
                    _Trips = base.CreateObjectSet<Trips>("Trips");
                }
                return _Trips;
            }
        }
        private ObjectSet<Trips> _Trips;
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<Users> Users
        {
            get
            {
                if ((_Users == null))
                {
                    _Users = base.CreateObjectSet<Users>("Users");
                }
                return _Users;
            }
        }
        private ObjectSet<Users> _Users;

        #endregion

        #region AddTo Methods
    
        /// <summary>
        /// Deprecated Method for adding a new object to the Locations EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToLocations(Locations locations)
        {
            base.AddObject("Locations", locations);
        }
    
        /// <summary>
        /// Deprecated Method for adding a new object to the M2MLocationsPhotos EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToM2MLocationsPhotos(M2MLocationsPhotos m2MLocationsPhotos)
        {
            base.AddObject("M2MLocationsPhotos", m2MLocationsPhotos);
        }
    
        /// <summary>
        /// Deprecated Method for adding a new object to the Photos EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToPhotos(Photos photos)
        {
            base.AddObject("Photos", photos);
        }
    
        /// <summary>
        /// Deprecated Method for adding a new object to the Trips EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToTrips(Trips trips)
        {
            base.AddObject("Trips", trips);
        }
    
        /// <summary>
        /// Deprecated Method for adding a new object to the Users EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToUsers(Users users)
        {
            base.AddObject("Users", users);
        }

        #endregion

    }

    #endregion

    #region Entities
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="PicPointDBModel", Name="Locations")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class Locations : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new Locations object.
        /// </summary>
        /// <param name="location_id">Initial value of the location_id property.</param>
        /// <param name="trip_id">Initial value of the trip_id property.</param>
        public static Locations CreateLocations(global::System.String location_id, global::System.String trip_id)
        {
            Locations locations = new Locations();
            locations.location_id = location_id;
            locations.trip_id = trip_id;
            return locations;
        }

        #endregion

        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String location_id
        {
            get
            {
                return _location_id;
            }
            set
            {
                if (_location_id != value)
                {
                    Onlocation_idChanging(value);
                    ReportPropertyChanging("location_id");
                    _location_id = StructuralObject.SetValidValue(value, false);
                    ReportPropertyChanged("location_id");
                    Onlocation_idChanged();
                }
            }
        }
        private global::System.String _location_id;
        partial void Onlocation_idChanging(global::System.String value);
        partial void Onlocation_idChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String trip_id
        {
            get
            {
                return _trip_id;
            }
            set
            {
                Ontrip_idChanging(value);
                ReportPropertyChanging("trip_id");
                _trip_id = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("trip_id");
                Ontrip_idChanged();
            }
        }
        private global::System.String _trip_id;
        partial void Ontrip_idChanging(global::System.String value);
        partial void Ontrip_idChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.Int32> coordinates
        {
            get
            {
                return _coordinates;
            }
            set
            {
                OncoordinatesChanging(value);
                ReportPropertyChanging("coordinates");
                _coordinates = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("coordinates");
                OncoordinatesChanged();
            }
        }
        private Nullable<global::System.Int32> _coordinates;
        partial void OncoordinatesChanging(Nullable<global::System.Int32> value);
        partial void OncoordinatesChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String story
        {
            get
            {
                return _story;
            }
            set
            {
                OnstoryChanging(value);
                ReportPropertyChanging("story");
                _story = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("story");
                OnstoryChanged();
            }
        }
        private global::System.String _story;
        partial void OnstoryChanging(global::System.String value);
        partial void OnstoryChanged();

        #endregion

    
    }
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="PicPointDBModel", Name="M2MLocationsPhotos")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class M2MLocationsPhotos : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new M2MLocationsPhotos object.
        /// </summary>
        /// <param name="entity_id">Initial value of the entity_id property.</param>
        /// <param name="location_id">Initial value of the location_id property.</param>
        public static M2MLocationsPhotos CreateM2MLocationsPhotos(global::System.String entity_id, global::System.String location_id)
        {
            M2MLocationsPhotos m2MLocationsPhotos = new M2MLocationsPhotos();
            m2MLocationsPhotos.entity_id = entity_id;
            m2MLocationsPhotos.location_id = location_id;
            return m2MLocationsPhotos;
        }

        #endregion

        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String entity_id
        {
            get
            {
                return _entity_id;
            }
            set
            {
                if (_entity_id != value)
                {
                    Onentity_idChanging(value);
                    ReportPropertyChanging("entity_id");
                    _entity_id = StructuralObject.SetValidValue(value, false);
                    ReportPropertyChanged("entity_id");
                    Onentity_idChanged();
                }
            }
        }
        private global::System.String _entity_id;
        partial void Onentity_idChanging(global::System.String value);
        partial void Onentity_idChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String location_id
        {
            get
            {
                return _location_id;
            }
            set
            {
                Onlocation_idChanging(value);
                ReportPropertyChanging("location_id");
                _location_id = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("location_id");
                Onlocation_idChanged();
            }
        }
        private global::System.String _location_id;
        partial void Onlocation_idChanging(global::System.String value);
        partial void Onlocation_idChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String photo_id
        {
            get
            {
                return _photo_id;
            }
            set
            {
                Onphoto_idChanging(value);
                ReportPropertyChanging("photo_id");
                _photo_id = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("photo_id");
                Onphoto_idChanged();
            }
        }
        private global::System.String _photo_id;
        partial void Onphoto_idChanging(global::System.String value);
        partial void Onphoto_idChanged();

        #endregion

    
    }
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="PicPointDBModel", Name="Photos")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class Photos : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new Photos object.
        /// </summary>
        /// <param name="photo_id">Initial value of the photo_id property.</param>
        public static Photos CreatePhotos(global::System.String photo_id)
        {
            Photos photos = new Photos();
            photos.photo_id = photo_id;
            return photos;
        }

        #endregion

        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String photo_id
        {
            get
            {
                return _photo_id;
            }
            set
            {
                if (_photo_id != value)
                {
                    Onphoto_idChanging(value);
                    ReportPropertyChanging("photo_id");
                    _photo_id = StructuralObject.SetValidValue(value, false);
                    ReportPropertyChanged("photo_id");
                    Onphoto_idChanged();
                }
            }
        }
        private global::System.String _photo_id;
        partial void Onphoto_idChanging(global::System.String value);
        partial void Onphoto_idChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String trip_id
        {
            get
            {
                return _trip_id;
            }
            set
            {
                Ontrip_idChanging(value);
                ReportPropertyChanging("trip_id");
                _trip_id = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("trip_id");
                Ontrip_idChanged();
            }
        }
        private global::System.String _trip_id;
        partial void Ontrip_idChanging(global::System.String value);
        partial void Ontrip_idChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String username
        {
            get
            {
                return _username;
            }
            set
            {
                OnusernameChanging(value);
                ReportPropertyChanging("username");
                _username = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("username");
                OnusernameChanged();
            }
        }
        private global::System.String _username;
        partial void OnusernameChanging(global::System.String value);
        partial void OnusernameChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.Byte[] photo
        {
            get
            {
                return StructuralObject.GetValidValue(_photo);
            }
            set
            {
                OnphotoChanging(value);
                ReportPropertyChanging("photo");
                _photo = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("photo");
                OnphotoChanged();
            }
        }
        private global::System.Byte[] _photo;
        partial void OnphotoChanging(global::System.Byte[] value);
        partial void OnphotoChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.Int32> coordinates
        {
            get
            {
                return _coordinates;
            }
            set
            {
                OncoordinatesChanging(value);
                ReportPropertyChanging("coordinates");
                _coordinates = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("coordinates");
                OncoordinatesChanged();
            }
        }
        private Nullable<global::System.Int32> _coordinates;
        partial void OncoordinatesChanging(Nullable<global::System.Int32> value);
        partial void OncoordinatesChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.Byte[] timestamp
        {
            get
            {
                return StructuralObject.GetValidValue(_timestamp);
            }
            set
            {
                OntimestampChanging(value);
                ReportPropertyChanging("timestamp");
                _timestamp = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("timestamp");
                OntimestampChanged();
            }
        }
        private global::System.Byte[] _timestamp;
        partial void OntimestampChanging(global::System.Byte[] value);
        partial void OntimestampChanged();

        #endregion

    
        #region Navigation Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("PicPointDBModel", "FK_Photos_Users", "Users")]
        public Users Users
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Users>("PicPointDBModel.FK_Photos_Users", "Users").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Users>("PicPointDBModel.FK_Photos_Users", "Users").Value = value;
            }
        }
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [BrowsableAttribute(false)]
        [DataMemberAttribute()]
        public EntityReference<Users> UsersReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Users>("PicPointDBModel.FK_Photos_Users", "Users");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<Users>("PicPointDBModel.FK_Photos_Users", "Users", value);
                }
            }
        }

        #endregion

    }
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="PicPointDBModel", Name="Trips")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class Trips : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new Trips object.
        /// </summary>
        /// <param name="trip_id">Initial value of the trip_id property.</param>
        public static Trips CreateTrips(global::System.String trip_id)
        {
            Trips trips = new Trips();
            trips.trip_id = trip_id;
            return trips;
        }

        #endregion

        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String trip_id
        {
            get
            {
                return _trip_id;
            }
            set
            {
                if (_trip_id != value)
                {
                    Ontrip_idChanging(value);
                    ReportPropertyChanging("trip_id");
                    _trip_id = StructuralObject.SetValidValue(value, false);
                    ReportPropertyChanged("trip_id");
                    Ontrip_idChanged();
                }
            }
        }
        private global::System.String _trip_id;
        partial void Ontrip_idChanging(global::System.String value);
        partial void Ontrip_idChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String username
        {
            get
            {
                return _username;
            }
            set
            {
                OnusernameChanging(value);
                ReportPropertyChanging("username");
                _username = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("username");
                OnusernameChanged();
            }
        }
        private global::System.String _username;
        partial void OnusernameChanging(global::System.String value);
        partial void OnusernameChanged();

        #endregion

    
        #region Navigation Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("PicPointDBModel", "FK_Trips_Users", "Users")]
        public Users Users
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Users>("PicPointDBModel.FK_Trips_Users", "Users").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Users>("PicPointDBModel.FK_Trips_Users", "Users").Value = value;
            }
        }
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [BrowsableAttribute(false)]
        [DataMemberAttribute()]
        public EntityReference<Users> UsersReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Users>("PicPointDBModel.FK_Trips_Users", "Users");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<Users>("PicPointDBModel.FK_Trips_Users", "Users", value);
                }
            }
        }

        #endregion

    }
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="PicPointDBModel", Name="Users")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class Users : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new Users object.
        /// </summary>
        /// <param name="username">Initial value of the username property.</param>
        /// <param name="password">Initial value of the password property.</param>
        public static Users CreateUsers(global::System.String username, global::System.String password)
        {
            Users users = new Users();
            users.username = username;
            users.password = password;
            return users;
        }

        #endregion

        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String username
        {
            get
            {
                return _username;
            }
            set
            {
                if (_username != value)
                {
                    OnusernameChanging(value);
                    ReportPropertyChanging("username");
                    _username = StructuralObject.SetValidValue(value, false);
                    ReportPropertyChanged("username");
                    OnusernameChanged();
                }
            }
        }
        private global::System.String _username;
        partial void OnusernameChanging(global::System.String value);
        partial void OnusernameChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String password
        {
            get
            {
                return _password;
            }
            set
            {
                OnpasswordChanging(value);
                ReportPropertyChanging("password");
                _password = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("password");
                OnpasswordChanged();
            }
        }
        private global::System.String _password;
        partial void OnpasswordChanging(global::System.String value);
        partial void OnpasswordChanged();

        #endregion

    
        #region Navigation Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("PicPointDBModel", "FK_Photos_Users", "Photos")]
        public EntityCollection<Photos> Photos
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<Photos>("PicPointDBModel.FK_Photos_Users", "Photos");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<Photos>("PicPointDBModel.FK_Photos_Users", "Photos", value);
                }
            }
        }
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("PicPointDBModel", "FK_Trips_Users", "Trips")]
        public EntityCollection<Trips> Trips
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<Trips>("PicPointDBModel.FK_Trips_Users", "Trips");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<Trips>("PicPointDBModel.FK_Trips_Users", "Trips", value);
                }
            }
        }

        #endregion

    }

    #endregion

    
}
