CREATE DATABASE BoardGameRental

CREATE TABLE CoreUser (
Id uniqueidentifier not null PRIMARY KEY,
Username varchar(255) not null,
Password varchar(255) not null,
Email varchar(255) not null,
DateCreated datetime not null,
DateUpdated datetime not null,
IsActive bit not null);

CREATE TABLE Role (
Id uniqueidentifier not null PRIMARY KEY,
Name varchar(255),
DateCreated datetime not null,
DateUpdated datetime not null,
CreatedByUserId uniqueidentifier not null,
UpdatedByUserId uniqueidentifier not null,
CONSTRAINT FK_Role_CoreUser_CreatedByUserId FOREIGN KEY (CreatedByUserId) REFERENCES CoreUser (Id),
CONSTRAINT FK_Role_CoreUser_UpdatedByUserId FOREIGN KEY (UpdatedByUserId) REFERENCES CoreUser (Id),
IsActive bit not null);

CREATE TABLE Category (
Id uniqueidentifier not null PRIMARY KEY,
Name varchar(255) not null,
Description text null,
DateCreated datetime not null,
DateUpdated datetime not null,
CreatedByUserId uniqueidentifier not null,
UpdatedByUserId uniqueidentifier not null,
CONSTRAINT FK_Category_CoreUser_CreatedByUserId FOREIGN KEY (CreatedByUserId) REFERENCES CoreUser (Id),
CONSTRAINT FK_Category_CoreUser_UpdatedByUserId FOREIGN KEY (UpdatedByUserId) REFERENCES CoreUser (Id),
IsActive bit not null);

CREATE TABLE BoardGame (
Id uniqueidentifier not null PRIMARY KEY,
Title varchar(255) not null,
Price dec(10,2) not null,
Description text null,
NumberOfCopies int not null,
DateCreated datetime not null,
DateUpdated datetime not null,
CreatedByUserId uniqueidentifier not null,
UpdatedByUserId uniqueidentifier not null,
IsActive bit not null,
CategoryId uniqueidentifier not null,
CONSTRAINT FK_BoardGame_Category_CategoryId FOREIGN KEY (CategoryId) REFERENCES Category (Id),
CONSTRAINT FK_BoardGame_CoreUser_CreatedByUserId FOREIGN KEY (CreatedByUserId) REFERENCES CoreUser (Id),
CONSTRAINT FK_BoardGame_CoreUser_UpdatedByUserId FOREIGN KEY (UpdatedByUserId) REFERENCES CoreUser (Id));

CREATE TABLE UsersDetail (
Id uniqueidentifier not null PRIMARY KEY,
FirstName varchar(255) not null,
LastName varchar(255) not null,
DOB date null,
Address varchar(255) null,
DateCreated datetime not null,
DateUpdated datetime not null,
CreatedByUserId uniqueidentifier not null,
UpdatedByUserId uniqueidentifier not null,
IsActive bit not null,
CONSTRAINT FK_UsersDetails_CoreUser_Id FOREIGN KEY (Id) REFERENCES CoreUser (Id),
CONSTRAINT FK_UsersDetails_CoreUser_CreatedByUserId FOREIGN KEY (CreatedByUserId) REFERENCES CoreUser (Id),
CONSTRAINT FK_UsersDetails_CoreUser_UpdatedByUserId FOREIGN KEY (UpdatedByUserId) REFERENCES CoreUser (Id));

CREATE TABLE RentedBoardGame (
Id uniqueidentifier not null PRIMARY KEY,
DateRented datetime not null,
DateReturned datetime null,
DateCreated datetime not null,
DateUpdated datetime not null,
CreatedByUserId uniqueidentifier not null,
UpdatedByUserId uniqueidentifier not null,
IsActive bit not null,
CoreUserId uniqueidentifier not null,
BoardGameId uniqueidentifier not null,
CONSTRAINT FK_RentedBoardGame_CoreUser_UserId FOREIGN KEY (CoreUserId) REFERENCES CoreUser (Id),
CONSTRAINT FK_RentedBoardGame_BoardGame_BoardGameId FOREIGN KEY (BoardGameId) REFERENCES BoardGame (Id),
CONSTRAINT FK_RentedBoardGame_CoreUser_CreatedByUserId FOREIGN KEY (CreatedByUserId) REFERENCES CoreUser (Id),
CONSTRAINT FK_RentedBoardGame_CoreUser_UpdatedByUserId FOREIGN KEY (UpdatedByUserId) REFERENCES CoreUser (Id));

CREATE TABLE Review (
Id uniqueidentifier not null PRIMARY KEY,
Title text not null,
Comment text null,
IsApproved bit not null,
Rating int not null,
DateCreated datetime not null,
DateUpdated datetime not null,
CreatedByUserId uniqueidentifier not null,
UpdatedByUserId uniqueidentifier not null,
IsActive bit not null,
CoreUserId uniqueidentifier not null,
BoardGameId uniqueidentifier not null,
CONSTRAINT FK_Review_CoreUser_UserId FOREIGN KEY (CoreUserId) REFERENCES CoreUser (Id),
CONSTRAINT FK_Review_BoardGame_BoardGameId FOREIGN KEY (BoardGameId) REFERENCES BoardGame (Id),
CONSTRAINT FK_Review_CoreUser_CreatedByUserId FOREIGN KEY (CreatedByUserId) REFERENCES CoreUser (Id),
CONSTRAINT FK_Review_CoreUser_UpdatedByUserId FOREIGN KEY (UpdatedByUserId) REFERENCES CoreUser (Id));

CREATE TABLE Notification (
Id uniqueidentifier not null PRIMARY KEY,
Title varchar(255) not null,
Message text not null,
DateCreated datetime not null,
DateUpdated datetime not null,
CreatedByUserId uniqueidentifier not null,
UpdatedByUserId uniqueidentifier not null,
IsActive bit not null,
CONSTRAINT FK_Notification_CoreUser_CreatedByUserId FOREIGN KEY (CreatedByUserId) REFERENCES CoreUser (Id),
CONSTRAINT FK_Notification_CoreUser_UpdatedByUserId FOREIGN KEY (UpdatedByUserId) REFERENCES CoreUser (Id));

CREATE TABLE UserNotification (
Id uniqueidentifier not null PRIMARY KEY,
DateCreated datetime not null,
DateUpdated datetime not null,
CreatedByUserId uniqueidentifier not null,
UpdatedByUserId uniqueidentifier not null,
IsActive bit not null,
IsRead bit not null,
CoreUserId uniqueidentifier not null,
NotificationId uniqueidentifier not null,
CONSTRAINT FK_UserNotification_CoreUser_CoreUserId FOREIGN KEY (CoreUserId) REFERENCES CoreUser (Id),
CONSTRAINT FK_UserNotification_Notification_NotificationId FOREIGN KEY (NotificationId) REFERENCES Notification (Id),
CONSTRAINT FK_UserNotification_CoreUser_CreatedByUserId FOREIGN KEY (CreatedByUserId) REFERENCES CoreUser (Id),
CONSTRAINT FK_UserNotification_CoreUser_UpdatedByUserId FOREIGN KEY (UpdatedByUserId) REFERENCES CoreUser (Id));

----------------------------------------------------------------------------------------

INSERT INTO CoreUser VALUES -- unesi bez roleId jer nije/nemoze jos bit dodan, rola za to nije kreirana
(newid(), 'ADMINISTRATOR', '1234', 'hotstuff@gmail.com', getdate(), getdate(), 1);
-- mora postojati ovaj odma na pocetku jer inace ne mozemo umetati njegov id za createdby i ostale

ALTER TABLE CoreUser  ------------------- zakompliciralo se, okidaj jedan po jedan blok
ADD
CreatedByUserId uniqueidentifier null,
UpdatedByUserId uniqueidentifier null,
CONSTRAINT FK_CoreUser_CoreUser_CreatedByUserId FOREIGN KEY (CreatedByUserId) REFERENCES CoreUser (Id),
CONSTRAINT FK_CoreUser_CoreUser_UpdatedByUserId FOREIGN KEY (UpdatedByUserId) REFERENCES CoreUser (Id);

UPDATE CoreUser 
SET CreatedByUserId = (SELECT "Id" FROM "CoreUser" WHERE Username like 'ADMINISTRATOR')
WHERE Username = 'ADMINISTRATOR';
UPDATE CoreUser 
SET UpdatedByUserId = (SELECT "Id" FROM "CoreUser" WHERE Username like 'ADMINISTRATOR')
WHERE Username = 'ADMINISTRATOR';

ALTER TABLE CoreUser
ALTER COLUMN CreatedByUserId uniqueidentifier not null

ALTER TABLE CoreUser
ALTER COLUMN UpdatedByUserId uniqueidentifier not null

-----------------------------------------------------------------------------------------
ALTER TABLE CoreUser --jer ako unesemo FK pri kreaciji tablice, ne vidi Role tablicu na koju se treba vezati jer jos nije kreirana
ADD 
RoleId uniqueidentifier null,
CONSTRAINT FK_CoreUser_Role_RoleId FOREIGN KEY (RoleId) REFERENCES Role (Id);

INSERT INTO Role VALUES -- pravimo rolu za admina, jer postoji user administrator pa se ovdje moze pozvati na njegov id, ali jos nismo dodali tom useru role id 
(newid(), 'SYSTEMADMIN', getdate(), getdate(), (SELECT "Id" FROM "CoreUser" WHERE Username like 'ADMINISTRATOR'), (SELECT "Id" FROM "CoreUser" WHERE Username like 'ADMINISTRATOR'), 1);

-- unosi id of admin role adminu u coreuser tablici
UPDATE CoreUser 
SET RoleId = (SELECT "Id" FROM "Role" WHERE Name like 'SYSTEMADMIN')
WHERE Username = 'ADMINISTRATOR';

ALTER TABLE CoreUser
ALTER COLUMN RoleId uniqueidentifier not null
-----------------------------------------------------------------------------------------------
--PUNIMO ROLE I USERE koji nisu admin
INSERT INTO Role VALUES
(newid(), 'User', getdate(), getdate(), (SELECT "Id" FROM "CoreUser" WHERE Username like 'ADMINISTRATOR'), (SELECT "Id" FROM "CoreUser" WHERE Username like 'ADMINISTRATOR'), 1),
(newid(), 'PremiumUser', getdate(), getdate(), (SELECT "Id" FROM "CoreUser" WHERE Username like 'ADMINISTRATOR'), (SELECT "Id" FROM "CoreUser" WHERE Username like 'ADMINISTRATOR'), 1);

INSERT INTO CoreUser VALUES
(newid(), 'kovac031', 'kovac', 'ivan.kovac.email@gmail.com', getdate(), getdate(), 1, (SELECT "Id" FROM "CoreUser" WHERE Username like 'ADMINISTRATOR'), (SELECT "Id" FROM "CoreUser" WHERE Username like 'ADMINISTRATOR'), (SELECT "Id" FROM "Role" WHERE Name like 'User')),
(newid(), 'barbaraBejb', 'barbara', 'barbarafriscic93@gmail.com', getdate(), getdate(), 1, (SELECT "Id" FROM "CoreUser" WHERE Username like 'ADMINISTRATOR'), (SELECT "Id" FROM "CoreUser" WHERE Username like 'ADMINISTRATOR'), (SELECT "Id" FROM "Role" WHERE Name like 'User')),
(newid(), 'lukaAgo', 'luka', 'agic.luke@gmail.com', getdate(), getdate(), 1, (SELECT "Id" FROM "CoreUser" WHERE Username like 'ADMINISTRATOR'), (SELECT "Id" FROM "CoreUser" WHERE Username like 'ADMINISTRATOR'), (SELECT "Id" FROM "Role" WHERE Name like 'User')),
(newid(), 'nikolaCes', 'nikola', 'agic.luke@gmail.com', getdate(), getdate(), 1, (SELECT "Id" FROM "CoreUser" WHERE Username like 'ADMINISTRATOR'), (SELECT "Id" FROM "CoreUser" WHERE Username like 'ADMINISTRATOR'), (SELECT "Id" FROM "Role" WHERE Name like 'User'));

INSERT INTO UsersDetail VALUES
((SELECT "Id" FROM "CoreUser" WHERE Username like 'kovac031'), 'Ivan', 'Kovac', '', '', getdate(), getdate(), (SELECT "Id" FROM "CoreUser" WHERE Username like 'ADMINISTRATOR'), (SELECT "Id" FROM "CoreUser" WHERE Username like 'ADMINISTRATOR'), 1),
((SELECT "Id" FROM "CoreUser" WHERE Username like 'barbaraBejb'), 'Barbara', 'Friscic', '', '', getdate(), getdate(), (SELECT "Id" FROM "CoreUser" WHERE Username like 'ADMINISTRATOR'), (SELECT "Id" FROM "CoreUser" WHERE Username like 'ADMINISTRATOR'), 1),
((SELECT "Id" FROM "CoreUser" WHERE Username like 'lukaAgo'), 'Luka', 'Agic', '', '', getdate(), getdate(), (SELECT "Id" FROM "CoreUser" WHERE Username like 'ADMINISTRATOR'), (SELECT "Id" FROM "CoreUser" WHERE Username like 'ADMINISTRATOR'), 1),
((SELECT "Id" FROM "CoreUser" WHERE Username like 'nikolaCes'), 'Nikola', 'Antovski', '', '', getdate(), getdate(), (SELECT "Id" FROM "CoreUser" WHERE Username like 'ADMINISTRATOR'), (SELECT "Id" FROM "CoreUser" WHERE Username like 'ADMINISTRATOR'), 1);

-------------------------------------------------------------------------------------------------

--Category
INSERT INTO Category VALUES
(newid(), 'Card', 'A card game is any game using playing cards as the primary device with which the game is played, be they traditional or game-specific.
Games using playing cards exploit the fact that cards are individually identifiable from one side only, so that each player knows only the cards they hold and not those held by anyone else.',
getdate(), getdate(), (SELECT "Id" FROM "CoreUser" WHERE Username like 'ADMINISTRATOR'), (SELECT "Id" FROM "CoreUser" WHERE Username like 'ADMINISTRATOR'), 1);

INSERT INTO Category VALUES
(newid(), 'Strategy', 'A strategy game or strategic game is a game (e.g. a board game) in which the players'' uncoerced, and often autonomous, decision-making skills 
have a high significance in determining the outcome. Almost all strategy games require internal decision tree-style thinking, and typically very high situational awareness.',
getdate(), getdate(), (SELECT "Id" FROM "CoreUser" WHERE Username like 'ADMINISTRATOR'), (SELECT "Id" FROM "CoreUser" WHERE Username like 'ADMINISTRATOR'), 1);

INSERT INTO Category VALUES
(newid(), 'Puzzle', 'Puzzle games are those in which the players are trying to solve a puzzle. Many Puzzle games require players to use problem solving, pattern recognition, 
organization and/or sequencing to reach their objectives.', getdate(), getdate(), 
(SELECT "Id" FROM "CoreUser" WHERE Username like 'ADMINISTRATOR'), (SELECT "Id" FROM "CoreUser" WHERE Username like 'ADMINISTRATOR'), 1);

--BoardGame
INSERT INTO BoardGame VALUES
(newid(), 'Catan', 8.49, 'Players take on the roles of settlers, each attempting to build and develop holdings while trading and acquiring resources. 
Players gain victory points as their settlements grow; the first to reach a set number of victory points.', 4, getdate(), getdate(),
(SELECT "Id" FROM "CoreUser" WHERE Username like 'ADMINISTRATOR'), (SELECT "Id" FROM "CoreUser" WHERE Username like 'ADMINISTRATOR'), 1, 
(SELECT "Id" FROM "Category" WHERE "Name" LIKE 'Strategy'));

INSERT INTO BoardGame VALUES
(newid(), 'Ubongo', 13.99, 'In Ubongo, players compete to solve individual puzzles as quickly as they can to get first crack at the gems on hand for the taking.', 1, 
getdate(), getdate(), (SELECT "Id" FROM "CoreUser" WHERE Username like 'ADMINISTRATOR'), (SELECT "Id" FROM "CoreUser" WHERE Username like 'ADMINISTRATOR'), 1, 
(SELECT "Id" FROM "Category" WHERE "Name" LIKE 'Puzzle'));

INSERT INTO BoardGame VALUES
(newid(), 'Dixit', 6.49, 'Using a deck of cards illustrated with dreamlike images, players select cards that match a title suggested by the designated storyteller player, 
and attempt to guess which card the storyteller selected.', 3, getdate(), getdate(),
(SELECT "Id" FROM "CoreUser" WHERE Username like 'ADMINISTRATOR'), (SELECT "Id" FROM "CoreUser" WHERE Username like 'ADMINISTRATOR'), 1, 
(SELECT "Id" FROM "Category" WHERE "Name" LIKE 'Card'));

INSERT INTO BoardGame VALUES
(newid(), 'Dobble', 9.99, 'Dobble is a game in which players have to find symbols in common between two cards.', 2, getdate(), getdate(),
(SELECT "Id" FROM "CoreUser" WHERE Username like 'ADMINISTRATOR'), (SELECT "Id" FROM "CoreUser" WHERE Username like 'ADMINISTRATOR'), 1, 
(SELECT "Id" FROM "Category" WHERE "Name" LIKE 'Card'));	

--------------------------------------------------------------------------
-- NEMOJ KIDAT OVO ISPOD POPRAVILI SMOOOO
ALTER TABLE BoardGame
ALTER COLUMN Price dec(10,2) not null

UPDATE BoardGame 
SET Price = 9.99
WHERE Title = 'Dobble';

UPDATE BoardGame 
SET Price = 6.49
WHERE Title = 'Dixit';

UPDATE BoardGame 
SET Price = 13.99
WHERE Title = 'Ubongo';

UPDATE BoardGame 
SET Price = 8.49
WHERE Title = 'Catan';

-------------------------------------------------
--duplicate email fix:
UPDATE CoreUser
SET Email = 'nikola.antovski@gmail.com'
WHERE Username = 'nikolaCes';

-------------------------------------------------
--seed RentedBoardGame:

	insert into RentedBoardGame values
	(newid(), '2023-04-10', '2023-04-10', getdate(), getdate(), (SELECT "Id" FROM "CoreUser" WHERE Username like 'ADMINISTRATOR'), (SELECT "Id" FROM "CoreUser" WHERE Username like 'ADMINISTRATOR'), 1, (SELECT "Id" FROM "CoreUser" WHERE Username like 'barbaraBejb'),(SELECT "Id" FROM "BoardGame" WHERE Title like 'Dobble') ),
	(newid(), '2023-04-08', '2023-04-08', getdate(), getdate(), (SELECT "Id" FROM "CoreUser" WHERE Username like 'ADMINISTRATOR'), (SELECT "Id" FROM "CoreUser" WHERE Username like 'ADMINISTRATOR'), 1, (SELECT "Id" FROM "CoreUser" WHERE Username like 'kovac031'),(SELECT "Id" FROM "BoardGame" WHERE Title like 'Catan') ),
	(newid(), '2023-04-11', '2023-04-11', getdate(), getdate(), (SELECT "Id" FROM "CoreUser" WHERE Username like 'ADMINISTRATOR'), (SELECT "Id" FROM "CoreUser" WHERE Username like 'ADMINISTRATOR'), 1, (SELECT "Id" FROM "CoreUser" WHERE Username like 'lukaAgo'),(SELECT "Id" FROM "BoardGame" WHERE Title like 'Catan') ),
	(newid(), '2023-04-05', '2023-04-05', getdate(), getdate(), (SELECT "Id" FROM "CoreUser" WHERE Username like 'ADMINISTRATOR'), (SELECT "Id" FROM "CoreUser" WHERE Username like 'ADMINISTRATOR'), 1, (SELECT "Id" FROM "CoreUser" WHERE Username like 'barbaraBejb'),(SELECT "Id" FROM "BoardGame" WHERE Title like 'Dixit') ),
	(newid(), '2023-03-10', '2023-03-20', getdate(), getdate(), (SELECT "Id" FROM "CoreUser" WHERE Username like 'ADMINISTRATOR'), (SELECT "Id" FROM "CoreUser" WHERE Username like 'ADMINISTRATOR'), 0, (SELECT "Id" FROM "CoreUser" WHERE Username like 'nikolaCes'),(SELECT "Id" FROM "BoardGame" WHERE Title like 'Dobble') ),
	(newid(), '2023-03-19', '2023-03-30', getdate(), getdate(), (SELECT "Id" FROM "CoreUser" WHERE Username like 'ADMINISTRATOR'), (SELECT "Id" FROM "CoreUser" WHERE Username like 'ADMINISTRATOR'), 0, (SELECT "Id" FROM "CoreUser" WHERE Username like 'kovac031'),(SELECT "Id" FROM "BoardGame" WHERE Title like 'Ubongo') );
