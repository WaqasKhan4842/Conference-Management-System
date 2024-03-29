--Aggregate Function -- 2
-- JOINS
-- Two tables -- 4
-- Three tables -- 1
-- Four tables -- 1
/* */
-- Subquery -- 3

-- Subquery
SELECT  con.*, (SELECT COUNT(*) FROM Abstracts WHERE con_ID = con.ID) AS AbstractCount 
FROM Conference con



-- Aggregate Functions

--1.
SELECT MAX(CONVERT(INT, SUBSTRING(ID, 4, LEN(ID)))) FROM Conference

--2.
SELECT COUNT(*) FROM Conference WHERE con_Name = @ConName AND org_ID = @OrgID.

--3. 





--JOINS
--Two table JOINS
SELECT 
       COUNT(DISTINCT CASE WHEN sr.rev_status = 'Done' THEN sr.sr_ID END) AS NumberOfReviews,
       COUNT(DISTINCT CASE WHEN sr.rev_status = 'Assigned' THEN sr.sr_ID END) AS NumberOfAssignedAbstracts
       FROM Suggested_Reviewer sr
       INNER JOIN Reviewer r ON sr.Rev_ID = r.Rev_ID
       WHERE r.email = @email;


SELECT Abstracts.*, Authors.* 
FROM Abstracts
INNER JOIN Authors ON Abstracts.auth_id = Authors.ID
WHERE Abstracts.con_ID  = @conference_ID


SELECT COUNT(*) AS NumberOfReviews
FROM Suggested_Reviewer sr
INNER JOIN Reviewer r ON sr.Rev_ID = r.Rev_ID
 WHERE r.Rev_ID = @reviewerId AND sr.rev_status = 'Done'

 INSERT INTO Suggested_Reviewer (sr_ID, Deadline, Rev_ID, abs_ID, rev_status)
 VALUES (@newSrId, DATEADD(day, -3, (SELECT con.con_Date 
                                     FROM Abstracts abs 
                                     INNER JOIN Conference con ON abs.con_ID = con.ID 
                                      WHERE abs.Id = @abstractId)),
         @reviewerId, @abstractId, @revStatus)



--Three table JOINS
--1.
SELECT c.*, s.Sp_Name, s.Degree, s.Works_at, s.Profession, o.org_name  
FROM Conference c  
INNER JOIN Speakers s ON c.ID = s.conf_ID
INNER JOIN Organizers o ON c.org_ID = o.org_ID;





--Four/Five table JOINS
SELECT SR.rev_Status, SR.Deadline, A.Id AS abs_ID, A.title, A.link, A.abs_Status, A.score, A.sub_Date, C.con_Name, C.about, O.org_name, AU.AutName, AU.email AS AuthorEmail, AU.profession, AU.works_at, AU.Degree, AU.Field
FROM Suggested_Reviewer SR
JOIN Abstracts A ON SR.abs_ID = A.Id
JOIN Conference C ON A.con_ID = C.ID
JOIN Organizers O ON C.org_ID = O.org_ID
JOIN Authors AU ON A.auth_id = AU.ID
WHERE SR.Rev_ID = @RevID AND SR.rev_Status = @RevStatus;
