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
						capacity INT);


DELETE FROM Conference
-- Trigger to ensure that the conference is 3 days in future.
CREATE TRIGGER CheckConferenceDate
ON Conference
INSTEAD OF INSERT
AS
BEGIN
    DECLARE @MinConferenceDate DATE;
    SET @MinConferenceDate = DATEADD(WEEK, 3, GETDATE());

    IF EXISTS (SELECT 1 FROM INSERTED WHERE con_Date < @MinConferenceDate)
    BEGIN
        PRINT 'ERROR: Conference date must be at least 3 weeks in the future.';
    END
    ELSE
    BEGIN
        INSERT INTO Conference
        SELECT * FROM INSERTED;
    END
END;




INSERT INTO Conference (ID, con_Name, con_Date, con_location, reg_start_date, reg_end_date, sub_start_date, sub_end_date, con_type, about, capacity)
VALUES ('con001', 'Test Conference', DATEADD(WEEK, 2, GETDATE()), 'Test Location', GETDATE(), DATEADD(DAY, 7, GETDATE()), DATEADD(DAY, 14, GETDATE()), DATEADD(DAY, 21, GETDATE()), 'Test Type', 'About the Test Conference', 100);
SELECT * FROM Conference

CREATE TABLE Reviewer(Rev_ID VARCHAR(10) PRIMARY KEY,
					  RevName VARCHAR(50),
					  email VARCHAR(50),
					  pass VARCHAR(50),
					  Expert_Field VARCHAR(50));
CREATE TRIGGER PreventReviewerDelete
ON Reviewer
INSTEAD OF DELETE
AS
BEGIN
    PRINT 'Deleting rows from the Reviewer table is not allowed.';
    ROLLBACK TRANSACTION;
END;


INSERT INTO Reviewer (Rev_ID, RevName, email, pass, Expert_Field)
VALUES ('Rev100', 'bilal', 'bilal@example.com', 'password123', 'Medicine');


DELETE FROM Reviewer WHERE Rev_ID = 'Rev100';