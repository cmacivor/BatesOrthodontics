CREATE TABLE [dbo].[DoctorReferral]
(
	[DoctorReferralID] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [DoctorFirstName] NVARCHAR(50) NOT NULL, 
    [DoctorLastName] NVARCHAR(50) NOT NULL, 
    [PracticeName] NVARCHAR(100) NOT NULL, 
    [DoctorEmail] NVARCHAR(50) NOT NULL, 
    [PatientFirstName] NVARCHAR(50) NOT NULL, 
    [PatientLastName] NVARCHAR(50) NOT NULL, 
    [PatientPhoneNumber] NCHAR(10) NOT NULL, 
    [PatientEmailAddress] NVARCHAR(50) NOT NULL, 
    [RadiographsSent] NCHAR(3) NOT NULL, 
    [Comments] NTEXT NOT NULL
)


CREATE TABLE [dbo].[PreferredAppointmentDay]
(
	[PreferredAppointmentDayID] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [AppointmentRequestDay] NCHAR(10) NULL, 
    [AppointmentRequestID] INT NULL
)

CREATE TABLE [dbo].[AppointmentRequest] (
    [AppointmentRequestID]      INT IDENTITY(1,1) NOT NULL,
    [FirstName]                 NVARCHAR (50)  NOT NULL,
    [LastName]                  NVARCHAR (50)  NOT NULL,
    [DOB]                       DATETIME2 (7)  NOT NULL,
    [ResponsiblePartyFirstName] NVARCHAR (50)  NOT NULL,
    [ResponsiblePartyLastName]  NVARCHAR (50)  NOT NULL,
    [IsNewPatient]              BIT            NOT NULL,
    [Phone]                     NVARCHAR (11)  NOT NULL,
    [Email]                     NVARCHAR (50)  NOT NULL,
    [PreferredModeOfContact]    NCHAR (10)     NOT NULL,
    [PreferredAppointmentDays]  INT            NOT NULL,
    [ConvenientTimes]           NVARCHAR (100) NOT NULL,
    [HowDidYouHear]             NVARCHAR (100) NULL,
    [GeneralDentistName]        NVARCHAR (50)  NULL,
    [AdditionalComments]        NVARCHAR (100) NULL,
    PRIMARY KEY CLUSTERED ([AppointmentRequestID] ASC)
);


CREATE TABLE [dbo].[FriendReferral]
(
	[FriendReferralID] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [FriendFirstName] NVARCHAR(50) NOT NULL, 
    [FriendLastName] NVARCHAR(50) NOT NULL, 
    [FriendEmail] NVARCHAR(50) NOT NULL, 
    [PatientFirstName] NVARCHAR(50) NOT NULL, 
    [PatientLastName] NVARCHAR(50) NOT NULL, 
    [PatientPhoneNumber] NCHAR(10) NOT NULL, 
    [PatientEmailAddress] NVARCHAR(50) NOT NULL, 
    [Relationship] NVARCHAR(50) NOT NULL 
)

CREATE TABLE [dbo].[SponsorshipRequest]
(
	[SponsorshipRequestID] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [Date] DATETIME2 NOT NULL, 
    [FirstName] NVARCHAR(50) NOT NULL, 
    [LastName] NVARCHAR(50) NOT NULL, 
    [PhoneNumber] NCHAR(10) NOT NULL, 
    [Address] NVARCHAR(50) NULL, 
    [AddressLine2] NVARCHAR(50) NULL, 
    [City] NVARCHAR(30) NULL, 
    [State] NCHAR(20) NULL, 
    [Zip] NCHAR(10) NULL, 
    [PatientTreatmentStatus] NVARCHAR(50) NOT NULL, 
    [Organization] NVARCHAR(50) NOT NULL, 
    [CheckPayableTo] NVARCHAR(50) NULL, 
    [SendCheckToAddress] NVARCHAR(50) NULL, 
    [SendCheckToAddress2] NVARCHAR(50) NULL, 
    [SendCheckToCity] NVARCHAR(50) NULL, 
    [SendCheckToState] NVARCHAR(50) NULL, 
    [SendCheckToZip] NCHAR(10) NULL, 
    [Comments] NTEXT NULL
)

CREATE TABLE [dbo].[SponsershipRequestAdType]
(
	[SponsorshipRequestAdTypeID] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [SponsorshipRequestID] INT NULL, 
    [AdType] NVARCHAR(50) NULL, 
    [AdTypeDescription] NVARCHAR(50) NULL, 
    [AdSize] NVARCHAR(50) NULL, 
    [AdCost] MONEY NULL, 
    [DueDate] DATETIME2 NULL
	FOREIGN KEY (SponsorshipRequestID) REFERENCES SponsorshipRequest(SponsorshipRequestID)
)


ALTER TABLE PreferredAppointmentDay
ADD FOREIGN KEY (AppointmentRequestID)
REFERENCES AppointmentRequest(AppointmentRequestID)





