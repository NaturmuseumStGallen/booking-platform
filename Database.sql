/*
 * Copyright (C) 2017 Naturmuseum St. Gallen
 *  > https://github.com/NaturmuseumStGallen
 *
 * Designed and engineered by Phantasus Software Systems
 *  > http://www.phantasus.ch
 *
 * This file is part of BookingPlatform.
 *
 * BookingPlatform is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * BookingPlatform is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with BookingPlatform. If not, see <http://www.gnu.org/licenses/>.
 */

/* --- Initialization --- */

DROP TABLE IF EXISTS Booking
DROP TABLE IF EXISTS DateRangeRule
DROP TABLE IF EXISTS EmailRecipient
DROP TABLE IF EXISTS Event2EventGroupRule
DROP TABLE IF EXISTS MinimumDateRule
DROP TABLE IF EXISTS Settings
DROP TABLE IF EXISTS [Time]
DROP TABLE IF EXISTS WeeklyRule

-- Tables which are referenced by foreign key constraints
DROP TABLE IF EXISTS AvailabilityStatus
DROP TABLE IF EXISTS EventGroupRule
DROP TABLE IF EXISTS [Event]
DROP TABLE IF EXISTS [Rule]
DROP TABLE IF EXISTS RuleType

/* --- Table Creation --- */

CREATE TABLE AvailabilityStatus
(
	Id INT NOT NULL PRIMARY KEY,
	Name VARCHAR(100) NOT NULL
)

CREATE TABLE [Event]
(
	Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	IsActive BIT NOT NULL,
	Name VARCHAR(100) NOT NULL,
	ColorComponentBlue INT NOT NULL,
	ColorComponentGreen INT NOT NULL,
	ColorComponentRed INT NOT NULL
)

CREATE TABLE Booking
(
	Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	EventId INT NOT NULL FOREIGN KEY REFERENCES [Event](Id),
	IsActive BIT NOT NULL,
	[Address] VARCHAR(100),
	[Date] DATETIME NOT NULL,
	Email VARCHAR(100) NOT NULL,
	FirstName VARCHAR(100) NOT NULL,
	Grade VARCHAR(100) NOT NULL,
	LastName VARCHAR(100) NOT NULL,
	Notes VARCHAR(5000),
	NumberOfKids INT NOT NULL,
	Phone VARCHAR(100) NOT NULL,
	School VARCHAR(100) NOT NULL,
	Town VARCHAR(100) NOT NULL,
	ZipCode INT
)

CREATE TABLE EmailRecipient
(
	Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[Address] VARCHAR(100) NOT NULL
)

CREATE TABLE RuleType
(
	Id INT NOT NULL PRIMARY KEY,
	Name VARCHAR(100) NOT NULL
)

CREATE TABLE [Rule]
(
	Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	RuleTypeId INT NOT NULL FOREIGN KEY REFERENCES RuleType(Id),
	Name VARCHAR(100) NOT NULL,
)

CREATE TABLE DateRangeRule
(
	Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	RuleId INT NOT NULL FOREIGN KEY REFERENCES [Rule](Id),
	AvailabilityStatusId INT NOT NULL FOREIGN KEY REFERENCES AvailabilityStatus(Id),
	EndDate DATE,
	EndTime TIME,
	StartDate DATE NOT NULL,
	StartTime TIME,
)

CREATE TABLE EventGroupRule
(
	Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	RuleId INT NOT NULL FOREIGN KEY REFERENCES [Rule](Id)
)

CREATE TABLE Event2EventGroupRule
(
	EventId INT NOT NULL FOREIGN KEY REFERENCES [Event](Id),
	EventGroupRuleId INT NOT NULL FOREIGN KEY REFERENCES EventGroupRule(Id),

	CONSTRAINT PK_Event2EventGroupRule PRIMARY KEY (EventId, EventGroupRuleId)
)

CREATE TABLE MinimumDateRule
(
	Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	RuleId INT NOT NULL FOREIGN KEY REFERENCES [Rule](Id),
	[Date] DATETIME,
	[Days] INT
)

CREATE TABLE WeeklyRule
(
	Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	RuleId INT NOT NULL FOREIGN KEY REFERENCES [Rule](Id),
	AvailabilityStatusId INT NOT NULL FOREIGN KEY REFERENCES AvailabilityStatus(Id),
	[DayOfWeek] INT NOT NULL,
	StartDate DATE,
	[Time] TIME
)

CREATE TABLE Settings
(
	PasswordHash VARCHAR(40) NOT NULL,
	PasswordSalt VARCHAR(32) NOT NULL,
	EmailTitle VARCHAR(100) NOT NULL,
	EmailHtmlContent VARCHAR(MAX) NOT NULL,
	EmailPlaintextContent VARCHAR(MAX) NOT NULL
)

CREATE TABLE [Time]
(
	Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[Value] TIME NOT NULL
)

/* --- Default Values --- */

INSERT INTO
	AvailabilityStatus(Id, Name)
VALUES
	(1, 'Free'),
	(2, 'Not Bookable'),
	(3, 'Booked')

INSERT INTO
	RuleType(Id, Name)
VALUES
	(1, 'Date Range'),
	(2, 'Event Group'),
	(3, 'Minimum Date'),
	(4, 'Weekly')

INSERT INTO
	Settings(PasswordHash, PasswordSalt, EmailTitle, EmailHtmlContent, EmailPlaintextContent)
VALUES
	('', '', '', '', '')
