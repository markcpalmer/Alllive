﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Alllive.Models
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="AllliveDB")]
	public partial class AllliveDBDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void Insertpassword(password instance);
    partial void Updatepassword(password instance);
    partial void Deletepassword(password instance);
    partial void InsertSchedule(Schedule instance);
    partial void UpdateSchedule(Schedule instance);
    partial void DeleteSchedule(Schedule instance);
    partial void InsertUser(User instance);
    partial void UpdateUser(User instance);
    partial void DeleteUser(User instance);
    #endregion
		
		public AllliveDBDataContext() : 
				base(global::System.Configuration.ConfigurationManager.ConnectionStrings["AllliveDBConnectionString"].ConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public AllliveDBDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public AllliveDBDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public AllliveDBDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public AllliveDBDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<password> passwords
		{
			get
			{
				return this.GetTable<password>();
			}
		}
		
		public System.Data.Linq.Table<Schedule> Schedules
		{
			get
			{
				return this.GetTable<Schedule>();
			}
		}
		
		public System.Data.Linq.Table<User> Users
		{
			get
			{
				return this.GetTable<User>();
			}
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.insertregistereduser")]
		public int insertregistereduser([global::System.Data.Linq.Mapping.ParameterAttribute(DbType="VarChar(50)")] string username, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="LastName", DbType="VarChar(255)")] string lastName, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="FirstName", DbType="VarChar(255)")] string firstName, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType="VarChar(16)")] string password)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), username, lastName, firstName, password);
			return ((int)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.insertscheduledmeeting")]
		public int insertscheduledmeeting(
					[global::System.Data.Linq.Mapping.ParameterAttribute(Name="SessionName", DbType="VarChar(100)")] string sessionName, 
					[global::System.Data.Linq.Mapping.ParameterAttribute(Name="Description", DbType="VarChar(250)")] string description, 
					[global::System.Data.Linq.Mapping.ParameterAttribute(Name="Date", DbType="Date")] System.Nullable<System.DateTime> date, 
					[global::System.Data.Linq.Mapping.ParameterAttribute(Name="StartTime", DbType="Time")] System.Nullable<System.TimeSpan> startTime, 
					[global::System.Data.Linq.Mapping.ParameterAttribute(Name="EndTime", DbType="Time")] System.Nullable<System.TimeSpan> endTime, 
					[global::System.Data.Linq.Mapping.ParameterAttribute(Name="TimeZone", DbType="Int")] System.Nullable<int> timeZone, 
					[global::System.Data.Linq.Mapping.ParameterAttribute(Name="Recurr", DbType="Bit")] System.Nullable<bool> recurr, 
					[global::System.Data.Linq.Mapping.ParameterAttribute(Name="Frequency", DbType="Int")] System.Nullable<int> frequency, 
					[global::System.Data.Linq.Mapping.ParameterAttribute(Name="RepeatEvery", DbType="Int")] System.Nullable<int> repeatEvery, 
					[global::System.Data.Linq.Mapping.ParameterAttribute(Name="RepeatFrequency", DbType="Int")] System.Nullable<int> repeatFrequency, 
					[global::System.Data.Linq.Mapping.ParameterAttribute(Name="Sunday", DbType="Bit")] System.Nullable<bool> sunday, 
					[global::System.Data.Linq.Mapping.ParameterAttribute(Name="Monday", DbType="Bit")] System.Nullable<bool> monday, 
					[global::System.Data.Linq.Mapping.ParameterAttribute(Name="Tuesday", DbType="Bit")] System.Nullable<bool> tuesday, 
					[global::System.Data.Linq.Mapping.ParameterAttribute(Name="Wednesday", DbType="Bit")] System.Nullable<bool> wednesday, 
					[global::System.Data.Linq.Mapping.ParameterAttribute(Name="Thursday", DbType="Bit")] System.Nullable<bool> thursday, 
					[global::System.Data.Linq.Mapping.ParameterAttribute(Name="Friday", DbType="Bit")] System.Nullable<bool> friday, 
					[global::System.Data.Linq.Mapping.ParameterAttribute(Name="Saturday", DbType="Bit")] System.Nullable<bool> saturday, 
					[global::System.Data.Linq.Mapping.ParameterAttribute(Name="RepeatMonthRadio1", DbType="Bit")] System.Nullable<bool> repeatMonthRadio1, 
					[global::System.Data.Linq.Mapping.ParameterAttribute(Name="RepeatMonthRadio2", DbType="Bit")] System.Nullable<bool> repeatMonthRadio2, 
					[global::System.Data.Linq.Mapping.ParameterAttribute(Name="Radio2List1", DbType="Int")] System.Nullable<int> radio2List1, 
					[global::System.Data.Linq.Mapping.ParameterAttribute(Name="Radio2List2", DbType="Int")] System.Nullable<int> radio2List2, 
					[global::System.Data.Linq.Mapping.ParameterAttribute(Name="EndDateBy", DbType="Date")] System.Nullable<System.DateTime> endDateBy, 
					[global::System.Data.Linq.Mapping.ParameterAttribute(Name="EndDateAfter", DbType="Date")] System.Nullable<System.DateTime> endDateAfter)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), sessionName, description, date, startTime, endTime, timeZone, recurr, frequency, repeatEvery, repeatFrequency, sunday, monday, tuesday, wednesday, thursday, friday, saturday, repeatMonthRadio1, repeatMonthRadio2, radio2List1, radio2List2, endDateBy, endDateAfter);
			return ((int)(result.ReturnValue));
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.password")]
	public partial class password : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _passwordID;
		
		private string _password1;
		
		private System.Nullable<int> _UserID;
		
		private EntityRef<User> _User;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnpasswordIDChanging(int value);
    partial void OnpasswordIDChanged();
    partial void Onpassword1Changing(string value);
    partial void Onpassword1Changed();
    partial void OnUserIDChanging(System.Nullable<int> value);
    partial void OnUserIDChanged();
    #endregion
		
		public password()
		{
			this._User = default(EntityRef<User>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_passwordID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int passwordID
		{
			get
			{
				return this._passwordID;
			}
			set
			{
				if ((this._passwordID != value))
				{
					this.OnpasswordIDChanging(value);
					this.SendPropertyChanging();
					this._passwordID = value;
					this.SendPropertyChanged("passwordID");
					this.OnpasswordIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="password", Storage="_password1", DbType="VarChar(16)")]
		public string password1
		{
			get
			{
				return this._password1;
			}
			set
			{
				if ((this._password1 != value))
				{
					this.Onpassword1Changing(value);
					this.SendPropertyChanging();
					this._password1 = value;
					this.SendPropertyChanged("password1");
					this.Onpassword1Changed();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UserID", DbType="Int")]
		public System.Nullable<int> UserID
		{
			get
			{
				return this._UserID;
			}
			set
			{
				if ((this._UserID != value))
				{
					if (this._User.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnUserIDChanging(value);
					this.SendPropertyChanging();
					this._UserID = value;
					this.SendPropertyChanged("UserID");
					this.OnUserIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="User_password", Storage="_User", ThisKey="UserID", OtherKey="UserID", IsForeignKey=true)]
		public User User
		{
			get
			{
				return this._User.Entity;
			}
			set
			{
				User previousValue = this._User.Entity;
				if (((previousValue != value) 
							|| (this._User.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._User.Entity = null;
						previousValue.passwords.Remove(this);
					}
					this._User.Entity = value;
					if ((value != null))
					{
						value.passwords.Add(this);
						this._UserID = value.UserID;
					}
					else
					{
						this._UserID = default(Nullable<int>);
					}
					this.SendPropertyChanged("User");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Schedule")]
	public partial class Schedule : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _SessionID;
		
		private System.Nullable<System.DateTime> _DateTimeScheduledIn;
		
		private System.Nullable<System.DateTime> _DateTimeScheduleOut;
		
		private int _UserID;
		
		private string _SessionDescription;
		
		private EntityRef<User> _User;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnSessionIDChanging(int value);
    partial void OnSessionIDChanged();
    partial void OnDateTimeScheduledInChanging(System.Nullable<System.DateTime> value);
    partial void OnDateTimeScheduledInChanged();
    partial void OnDateTimeScheduleOutChanging(System.Nullable<System.DateTime> value);
    partial void OnDateTimeScheduleOutChanged();
    partial void OnUserIDChanging(int value);
    partial void OnUserIDChanged();
    partial void OnSessionDescriptionChanging(string value);
    partial void OnSessionDescriptionChanged();
    #endregion
		
		public Schedule()
		{
			this._User = default(EntityRef<User>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SessionID", DbType="Int NOT NULL", IsPrimaryKey=true)]
		public int SessionID
		{
			get
			{
				return this._SessionID;
			}
			set
			{
				if ((this._SessionID != value))
				{
					this.OnSessionIDChanging(value);
					this.SendPropertyChanging();
					this._SessionID = value;
					this.SendPropertyChanged("SessionID");
					this.OnSessionIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DateTimeScheduledIn", DbType="DateTime")]
		public System.Nullable<System.DateTime> DateTimeScheduledIn
		{
			get
			{
				return this._DateTimeScheduledIn;
			}
			set
			{
				if ((this._DateTimeScheduledIn != value))
				{
					this.OnDateTimeScheduledInChanging(value);
					this.SendPropertyChanging();
					this._DateTimeScheduledIn = value;
					this.SendPropertyChanged("DateTimeScheduledIn");
					this.OnDateTimeScheduledInChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DateTimeScheduleOut", DbType="DateTime")]
		public System.Nullable<System.DateTime> DateTimeScheduleOut
		{
			get
			{
				return this._DateTimeScheduleOut;
			}
			set
			{
				if ((this._DateTimeScheduleOut != value))
				{
					this.OnDateTimeScheduleOutChanging(value);
					this.SendPropertyChanging();
					this._DateTimeScheduleOut = value;
					this.SendPropertyChanged("DateTimeScheduleOut");
					this.OnDateTimeScheduleOutChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UserID", AutoSync=AutoSync.Always, DbType="Int NOT NULL IDENTITY", IsDbGenerated=true)]
		public int UserID
		{
			get
			{
				return this._UserID;
			}
			set
			{
				if ((this._UserID != value))
				{
					if (this._User.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnUserIDChanging(value);
					this.SendPropertyChanging();
					this._UserID = value;
					this.SendPropertyChanged("UserID");
					this.OnUserIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SessionDescription", DbType="NVarChar(50)")]
		public string SessionDescription
		{
			get
			{
				return this._SessionDescription;
			}
			set
			{
				if ((this._SessionDescription != value))
				{
					this.OnSessionDescriptionChanging(value);
					this.SendPropertyChanging();
					this._SessionDescription = value;
					this.SendPropertyChanged("SessionDescription");
					this.OnSessionDescriptionChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="User_Schedule", Storage="_User", ThisKey="UserID", OtherKey="UserID", IsForeignKey=true)]
		public User User
		{
			get
			{
				return this._User.Entity;
			}
			set
			{
				User previousValue = this._User.Entity;
				if (((previousValue != value) 
							|| (this._User.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._User.Entity = null;
						previousValue.Schedules.Remove(this);
					}
					this._User.Entity = value;
					if ((value != null))
					{
						value.Schedules.Add(this);
						this._UserID = value.UserID;
					}
					else
					{
						this._UserID = default(int);
					}
					this.SendPropertyChanged("User");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Users")]
	public partial class User : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _UserID;
		
		private string _UserName;
		
		private string _LastName;
		
		private string _FirstName;
		
		private EntitySet<password> _passwords;
		
		private EntitySet<Schedule> _Schedules;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnUserIDChanging(int value);
    partial void OnUserIDChanged();
    partial void OnUserNameChanging(string value);
    partial void OnUserNameChanged();
    partial void OnLastNameChanging(string value);
    partial void OnLastNameChanged();
    partial void OnFirstNameChanging(string value);
    partial void OnFirstNameChanged();
    #endregion
		
		public User()
		{
			this._passwords = new EntitySet<password>(new Action<password>(this.attach_passwords), new Action<password>(this.detach_passwords));
			this._Schedules = new EntitySet<Schedule>(new Action<Schedule>(this.attach_Schedules), new Action<Schedule>(this.detach_Schedules));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UserID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int UserID
		{
			get
			{
				return this._UserID;
			}
			set
			{
				if ((this._UserID != value))
				{
					this.OnUserIDChanging(value);
					this.SendPropertyChanging();
					this._UserID = value;
					this.SendPropertyChanged("UserID");
					this.OnUserIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UserName", DbType="VarChar(50)")]
		public string UserName
		{
			get
			{
				return this._UserName;
			}
			set
			{
				if ((this._UserName != value))
				{
					this.OnUserNameChanging(value);
					this.SendPropertyChanging();
					this._UserName = value;
					this.SendPropertyChanged("UserName");
					this.OnUserNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LastName", DbType="VarChar(255)")]
		public string LastName
		{
			get
			{
				return this._LastName;
			}
			set
			{
				if ((this._LastName != value))
				{
					this.OnLastNameChanging(value);
					this.SendPropertyChanging();
					this._LastName = value;
					this.SendPropertyChanged("LastName");
					this.OnLastNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FirstName", DbType="VarChar(255)")]
		public string FirstName
		{
			get
			{
				return this._FirstName;
			}
			set
			{
				if ((this._FirstName != value))
				{
					this.OnFirstNameChanging(value);
					this.SendPropertyChanging();
					this._FirstName = value;
					this.SendPropertyChanged("FirstName");
					this.OnFirstNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="User_password", Storage="_passwords", ThisKey="UserID", OtherKey="UserID")]
		public EntitySet<password> passwords
		{
			get
			{
				return this._passwords;
			}
			set
			{
				this._passwords.Assign(value);
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="User_Schedule", Storage="_Schedules", ThisKey="UserID", OtherKey="UserID")]
		public EntitySet<Schedule> Schedules
		{
			get
			{
				return this._Schedules;
			}
			set
			{
				this._Schedules.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_passwords(password entity)
		{
			this.SendPropertyChanging();
			entity.User = this;
		}
		
		private void detach_passwords(password entity)
		{
			this.SendPropertyChanging();
			entity.User = null;
		}
		
		private void attach_Schedules(Schedule entity)
		{
			this.SendPropertyChanging();
			entity.User = this;
		}
		
		private void detach_Schedules(Schedule entity)
		{
			this.SendPropertyChanging();
			entity.User = null;
		}
	}
}
#pragma warning restore 1591
