
--Admin Table
CREATE TABLE Admin( ID INT PRIMARY KEY,
					adName VARCHAR(50),
					email VARCHAR(50),
					pass VARCHAR(50));
INSERT INTO Admin VALUES
(100,'Waqas','wkhh70709@gmail.com',123456),
(101,'Awais','awais70709@gmail.com',123456);
CREATE VIEW AdminView
AS
SELECT ID, adName, email
FROM Admin;

SELECT * FROM AdminView

-- Reviewver Table
--Rev100
CREATE TABLE Reviewer(Rev_ID VARCHAR(10) PRIMARY KEY,
					  RevName VARCHAR(50),
					  email VARCHAR(50),
					  pass VARCHAR(50),
					  Expert_Field VARCHAR(50));
-- Organizers Table
--Org100
CREATE TABLE Organizers(org_ID VARCHAR(10) PRIMARY KEY,
						org_name VARCHAR(50),
						email VARCHAR(50),
						pass VARCHAR(50));
INSERT INTO Organizers VALUES
('Org101','BrainStorm Ltd', 'bs@gmail.com','123456');
--('Org100','Cortex Ltd', 'cortex@gmail.com','123456');

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

-- Conference Table
-- CON100
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
						FOREIGN KEY (org_ID) REFERENCES Organizers(org_ID) ON UPDATE CASCADE ON DELETE CASCADE);
-- Create the trigger
CREATE TRIGGER CheckConferenceDate
ON Conference
BEFORE INSERT
AS
BEGIN
    -- Check if the inserted conference date is at least 3 weeks in the future
    IF EXISTS (
        SELECT 1
        FROM inserted
        WHERE DATEDIFF(WEEK, GETDATE(), inserted.con_Date) < 3
    )
    BEGIN
        -- If the condition is not met, raise an error and rollback the transaction
       print 'Conference date must be at least 3 weeks in the future.', 16, 1;
        ROLLBACK;
    END
END;


-- Speaker Table
--spk100
CREATE TABLE Speakers(ID VARCHAR(10) PRIMARY KEY,
					  Sp_Name VARCHAR(50),
					  Degree VARCHAR(50),
					  Works_at VARCHAR(200),
					  Profession VARCHAR(50),
					  conf_ID VARCHAR(10),
					  FOREIGN KEY (conf_ID) REFERENCES Conference(ID)ON UPDATE CASCADE ON DELETE CASCADE);
-- Abstract Table
CREATE TABLE Abstracts(Id INT PRIMARY KEY,
					  title VARCHAR(50),
					  link VARCHAR(250),
					  sub_Date DATE,
					  auth_id VARCHAR(10),
					  abs_Status VARCHAR(15),
					  con_ID VARCHAR(10),
					  score INT,
					  FOREIGN KEY (con_ID) REFERENCES Conference(ID) ON UPDATE CASCADE ON DELETE CASCADE,
					  FOREIGN KEY (auth_id) REFERENCES  Authors(ID) ON UPDATE CASCADE ON DELETE CASCADE);

--Suggested Reviewer
DELETE FROM Suggested_Reviewer
CREATE TABLE Suggested_Reviewer(Deadline DATE,
								Rev_ID VARCHAR(10),
								sr_ID INT PRIMARY KEY,
								abs_ID INT,
								rev_status VARCHAR(15),
								FOREIGN KEY (Rev_ID) REFERENCES Reviewer(Rev_ID) ON UPDATE CASCADE ON DELETE CASCADE,
								FOREIGN KEY (abs_ID) REFERENCES Abstracts(Id) ON UPDATE CASCADE ON DELETE CASCADE);

-- Relation Table between Attendees and Conference.
CREATE TABLE At_Conference(con_ID VARCHAR(10),
						   email VARCHAR(50),
						   FOREIGN KEY (con_ID) REFERENCES Conference(ID) ON UPDATE CASCADE ON DELETE CASCADE,
						   FOREIGN KEY (email) REFERENCES  Attendee(email)  ON UPDATE CASCADE ON DELETE CASCADE);



CREATE TABLE Reviews(review_id INT PRIMARY KEY,
					 score INT,
					 suggested_reviewer_id INT,
					 abs_ID INT,
					 FOREIGN KEY (suggested_reviewer_id) REFERENCES Suggested_Reviewer(sr_ID) ,
					 FOREIGN KEY (abs_ID) REFERENCES Abstracts(Id)ON UPDATE CASCADE ON DELETE CASCADE);


-- Accepted Abstracts 
CREATE TABLE Accepted_Abstracts(abs_ID INT PRIMARY KEY,
								FOREIGN KEY (abs_ID) REFERENCES Abstracts(Id)ON UPDATE CASCADE ON DELETE CASCADE);
-- Presentation
CREATE TABLE Presentation(ID INT PRIMARY KEY,
						  abs_ID INT,
						  Title VARCHAR(200),
						  pre_time TIME,
						  pre_Date DATE,
						  pre_location VARCHAR(200),
						  conf_ID VARCHAR(10),
						  FOREIGN KEY (abs_ID) REFERENCES Accepted_Abstracts(abs_ID)ON UPDATE CASCADE ON DELETE CASCADE,
						  FOREIGN KEY (conf_ID) REFERENCES Conference(ID)ON UPDATE CASCADE ON DELETE CASCADE);


SELECT * FROM Reviewer

--Aggregate Functions
--1.
SELECT COUNT(*) FROM At_Conference WHERE con_ID = 'Whatever ID is given by the user';
SELECT ISNULL(MAX(CAST(SUBSTRING(ID, 4, LEN(ID) - 3) AS INT)), 99) + 1 FROM Authors


--Nested Subuery
--1.
SELECT 
con.*, 
(SELECT COUNT(*) FROM Abstracts WHERE con_ID = con.ID) AS AbstractCount 
FROM Conference con

--2
INSERT INTO Suggested_Reviewer (sr_ID, Deadline, Rev_ID, abs_ID, rev_status)
                                VALUES ('ID', DATEADD(day, -3, (SELECT con.con_Date 
                                                                    FROM Abstracts abs 
                                                                    INNER JOIN Conference con ON abs.con_ID = con.ID 
                                                                    WHERE abs.Id = 'AbsID')),
                                            'reviewerId', 'abstractId', 'revStatus')
SELECT * FROM Suggested_Reviewer
SELECT * FROM Abstracts
UPDATE Abstracts
SET score = 50
WHERE ID = 100
use DB
DELETE FROM Accepted_Abstracts
--3.
SELECT COUNT(DISTINCT CASE WHEN sr.rev_status = 'Done' THEN sr.sr_ID END) AS NumberOfReviews,
       COUNT(DISTINCT CASE WHEN sr.rev_status = 'Assigned' THEN sr.sr_ID END) AS NumberOfAssignedAbstracts
       FROM Suggested_Reviewer sr
       INNER JOIN Reviewer r ON sr.Rev_ID = r.Rev_ID
       WHERE r.email = 'bilal@gmail.com'

--4
CREATE VIEW CountAuthorsSubmmission
AS
SELECT
    A.AutName AS AuthorName,
    A.ID AS AuthorID
FROM
    Authors A
WHERE
    (SELECT COUNT(DISTINCT con_ID) FROM Abstracts WHERE auth_id = A.ID) > 1;



-- Two Tables Join
--1.
CREATE VIEW ConferenceWithOrganizersView
AS
SELECT
    c.*,
    o.org_name
FROM
    Conference c
    INNER JOIN Organizers o ON c.org_ID = o.org_ID;

-- Database:
SELECT * FROM ConferenceWithOrganizersView.


-- 2.
CREATE VIEW AbstractsWithAuthorsForConferenceView
AS
SELECT
    a.ID AS abstractID,
    a.title,
    a.link,
    a.abs_Status,
    a.score,
    a.sub_Date,
    a.con_ID,
    a.auth_id,
    au.ID AS authorID,
    au.AutName,
    au.email AS AuthorEmail,
    au.profession,
    au.works_at,
    au.Degree,
    au.Field
FROM
    Abstracts a
    INNER JOIN Authors au ON a.auth_id = au.ID;



--3.

CREATE VIEW ReviewCountView
AS
SELECT
    r.Rev_ID AS ReviewerID,
    sr.rev_status AS ReviewStatus,
    COUNT(*) AS NumberOfReviews
FROM
    Suggested_Reviewer sr
    INNER JOIN Reviewer r ON sr.Rev_ID = r.Rev_ID
GROUP BY
    r.Rev_ID, sr.rev_status;



--4.
SELECT COUNT(*) AS NumberOfAssignedAbstracts
FROM Suggested_Reviewer sr
INNER JOIN Reviewer r ON sr.Rev_ID = r.Rev_ID
WHERE r.Rev_ID = 'reviewerId' AND sr.rev_status = 'Assigned'



--Four/Five Table join
--CREATE AND JOIN.
CREATE VIEW Assigned_Abstracts_View 
AS
SELECT SR.rev_Status, SR.Deadline, A.Id AS abs_ID, A.title, A.link, A.abs_Status, A.score, A.sub_Date, C.con_Name, C.about, O.org_name, AU.AutName, AU.email AS AuthorEmail, AU.profession, AU.works_at, AU.Degree, AU.Field
FROM Suggested_Reviewer SR
JOIN Abstracts A ON SR.abs_ID = A.Id
JOIN Conference C ON A.con_ID = C.ID 
JOIN Organizers O ON C.org_ID = O.org_ID 
JOIN Authors AU ON A.auth_id = AU.ID 

--2.
CREATE VIEW ConferenceAverageScores AS
SELECT c.ID AS ConferenceID, c.con_Name, AVG(r.score) AS AverageScore
FROM Conference c
JOIN Abstracts a ON c.ID = a.con_ID
JOIN Accepted_Abstracts aa ON a.Id = aa.abs_ID
JOIN Reviews r ON aa.abs_ID = r.abs_ID
GROUP BY c.ID, c.con_Name
HAVING COUNT(aa.abs_ID) >= 2;


--Three table Joins
--1.
SELECT R.RevName
FROM Reviewer R
JOIN Suggested_Reviewer SR ON R.Rev_ID = SR.Rev_ID
JOIN Abstracts A ON SR.abs_ID = A.Id;


--2.
CREATE VIEW AuthorReviewSummaryView
AS
SELECT
    A.ID AS AuthorID,
    A.AutName,
    AVG(R.score) AS AverageReviewScore
FROM
    Authors A
LEFT JOIN
    Abstracts Abs ON A.ID = Abs.auth_id
LEFT JOIN
    Reviews R ON Abs.ID = R.abs_ID
GROUP BY
    A.ID, A.AutName;
SELECT * FROM AuthorReviewSummaryView

--3.
SELECT
    C.ID AS ConferenceID,
    C.con_Name AS ConferenceName,
    COUNT(AA.abs_ID) AS AcceptedAbstractCount,
    AVG(R.score) AS AverageAbstractScore
FROM
    Conference C
JOIN
    Abstracts A ON C.ID = A.con_ID
JOIN
    Accepted_Abstracts AA ON A.ID = AA.abs_ID
LEFT JOIN
    Reviews R ON AA.abs_ID = R.abs_ID
GROUP BY
    C.ID, C.con_Name;




-- in database:
SELECT * FROM Assigned_Abstracts_View

Select Top 1 ID From Authors, AutName From Authors a
Join Abstracts ab
On ab.ID = a.ID
Join Accepted_Abstracts aa
On aa.abs_ID = ab.ID

