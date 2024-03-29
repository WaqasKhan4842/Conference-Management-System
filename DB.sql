
use DB
DROP TABLE IF EXISTS Admin
DROP TABLE IF EXISTS Reviewer
DROP TABLE IF EXISTS Organizers
DROP TABLE IF EXISTS Authors
DROP TABLE IF EXISTS Attendee


--Admin Table
CREATE TABLE Admin( ID INT PRIMARY KEY,
					adName VARCHAR(50),
					email VARCHAR(50),
					pass VARCHAR(50));
INSERT INTO Admin VALUES
(100,'Waqas','wkhh70709@gmail.com',123456),
(101,'Awais','awais70709@gmail.com',123456);

-- Reviewver Table
--Rev100
SELECT 
CREATE TABLE Reviewer(Rev_ID VARCHAR(10) PRIMARY KEY,
					  RevName VARCHAR(50),
					  email VARCHAR(50),
					  pass VARCHAR(50),
					  Expert_Field VARCHAR(50));
					  SELECT * FROM Reviewer
-- Organizers Table
--Org100
CREATE TABLE Organizers(org_ID VARCHAR(10) PRIMARY KEY,
						org_name VARCHAR(50),
						email VARCHAR(50),
						pass VARCHAR(50));
INSERT INTO Organizers VALUES
('Org100','Cortex Ltd', 'cortex@gmail.com','123456');

SELECT * FROM Organizers

-- Authors Table
--Aut100 
CREATE TABLE Authors(ID VARCHAR(10) PRIMARY KEY,
					 AutName VARCHAR(50),
					 email VARCHAR(50),
					 pass VARCHAR(50),
					 profession VARCHAR(50),
					 works_at VARCHAR(50),
					 Degree VARCHAR(50),
					 Field VARCHAR(50));

-- Attendee Table.
CREATE TABLE Attendee(Att_Name VARCHAR(50),
					  email VARCHAR(50) PRIMARY KEY,
					  pass VARCHAR(50),
					  profession VARCHAR(50),
					  works_at VARCHAR(50),
					  Degree VARCHAR(50));
SELECT * FROM Attendee
SELECT * FROM At_Conference
DELETE FROM At_Conference
DROP TABLE Attendee
SELECT * FROM Authors

-- Conference Table
-- CON100
SELECT * FROM Organizers
DROP TABLE Conference
CREATE TABLE Conference(ID VARCHAR(10) PRIMARY KEY,
						con_Name VARCHAR(200),
						con_Date DATE,
						con_location VARCHAR(50),
						reg_start_date DATE,
						reg_end_date DATE,
						sub_start_date DATE,
						sub_end_date DATE,
						con_type VARCHAR(50),
						about VARCHAR(150),
						capacity INT,
						org_ID VARCHAR(10),
						FOREIGN KEY (org_ID) REFERENCES Organizers(org_ID));

CREATE TRIGGER ConferenceDateCheck
ON Conference
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @CurrentDate DATETIME = GETDATE();
    DECLARE @MinFutureDate DATETIME = DATEADD(WEEK, 4, @CurrentDate);

    IF EXISTS (
        SELECT 1
        FROM inserted
        WHERE con_Date <= @CurrentDate
    )
    BEGIN
        print 'The conference date must be at least 4 weeks in the future.';
        ROLLBACK;
    END
END;
INSERT INTO Conference (
    ID,
    con_Name,
    con_Date,
    con_location,
    reg_start_date,
    reg_end_date,
    sub_start_date,
    sub_end_date,
    con_type,
    about,
    capacity,
    org_ID
)
VALUES (
    '2',
    'Invalid Conference',
    DATEADD(WEEK, 3, GETDATE()),  -- Setting the conference date less than 4 weeks in the future
    'Invalid Venue',
    GETDATE(),
    DATEADD(DAY, 30, GETDATE()),
    DATEADD(DAY, 10, GETDATE()),
    DATEADD(DAY, 20, GETDATE()),
    'Invalid Type',
    'Description of the invalid conference',
    50,
    'Org100'
);
SELECT * FROM Conference
DELETE FROM Conference
WHERE ID = '2'
-- Speaker Table
--spk100
CREATE TABLE Speakers(ID VARCHAR(10) PRIMARY KEY,
					  Sp_Name VARCHAR(50),
					  Degree VARCHAR(50),
					  Works_at VARCHAR(200),
					  Profession VARCHAR(50),
					  conf_ID VARCHAR(10),
					  FOREIGN KEY (conf_ID) REFERENCES Conference(ID));
			
-- Abstract Table
CREATE TABLE Abstracts(Id INT PRIMARY KEY,
					  title VARCHAR(50),
					  link VARCHAR(250),
					  sub_Date DATE,
					  auth_id VARCHAR(10),
					  abs_Status VARCHAR(15),
					  con_ID VARCHAR(10),
					  score INT,
					  FOREIGN KEY (con_ID) REFERENCES Conference(ID),
					  FOREIGN KEY (auth_id) REFERENCES  Authors(ID));
use DB
SELECT * FROM Abstracts
SELECT * FROM Suggested_Reviewer
SELECT * FROM Reviewer
DELETE FROM Reviews
SELECT * FROM Reviews

--Suggested Reviewer
CREATE TABLE Suggested_Reviewer(Deadline DATE,
								Rev_ID VARCHAR(10),
								sr_ID INT PRIMARY KEY,
								abs_ID INT,
								rev_status VARCHAR(15),
								FOREIGN KEY (Rev_ID) REFERENCES Reviewer(Rev_ID),
								FOREIGN KEY (abs_ID) REFERENCES Abstracts(Id));

							

SELECT * FROM Conference
UPDATE Conference
SET capacity = 2
WHERE ID = 'CON100'
SELECT * FROM Attendee
SELECT * FROM At_Conference

-- Relation Table between Attendees and Conference.
CREATE TABLE At_Conference(con_ID VARCHAR(10),
						   email VARCHAR(50),
						   FOREIGN KEY (con_ID) REFERENCES Conference(ID),
						   FOREIGN KEY (email) REFERENCES  Attendee(email));
SELECT * FROM Speakers
						   SELECT * FROM Reviewer
CREATE TABLE Reviews(review_id INT PRIMARY KEY,
					 score INT,
					 suggested_reviewer_id INT,
					 abs_ID INT,
					 FOREIGN KEY (suggested_reviewer_id) REFERENCES Suggested_Reviewer(sr_ID),
					 FOREIGN KEY (abs_ID) REFERENCES Abstracts(Id));
CREATE TABLE Accepted_Abstracts(abs_ID INT PRIMARY KEY,
								FOREIGN KEY (abs_ID) REFERENCES Abstracts(Id));

CREATE TABLE Presentation(ID INT PRIMARY KEY,
						  abs_ID INT,
						  Title VARCHAR(200),
						  pre_time TIME,
						  pre_Date DATE,
						  pre_location VARCHAR(200),
						  conf_ID VARCHAR(10),
						  FOREIGN KEY (abs_ID) REFERENCES Accepted_Abstracts(abs_ID),
						  FOREIGN KEY (conf_ID) REFERENCES Conference(ID));



SELECT * FROM Reviewer
SELECT * FROM Reviews
SELECT * FROM Suggested_Reviewer
UPDATE Suggested_Reviewer
SET rev_status = 'Assigned'
WHERE sr_ID = 4
SELECT * FROM Abstracts
UPDATE Abstracts
SET score = 50
WHERE score = 75
SELECT * FROM Accepted_Abstracts
UPDATE Abstracts
SET score = 50
WHERE Id = 100

	SELECT * FROM Reviewer




