-- Create the Persons table
CREATE TABLE IF NOT EXISTS "Persons" (
    "Id" INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    "IsActive" INTEGER,
    "Name" TEXT NOT NULL
);

-- Create the Questions table
CREATE TABLE IF NOT EXISTS "Questions" (
    "Id" INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    "IsActive" INTEGER,
    "PersonId" INTEGER,
    "Text" TEXT NOT NULL,
    FOREIGN KEY ("PersonId") REFERENCES "Persons" ("Id")
);

-- Create the Answers table
CREATE TABLE IF NOT EXISTS "Answers" (
    "Id" INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    "PersonId" INTEGER,
    "QuestionId" INTEGER NOT NULL,
    "Text" TEXT NOT NULL,
    FOREIGN KEY ("PersonId") REFERENCES "Persons" ("Id"),
    FOREIGN KEY ("QuestionId") REFERENCES "Questions" ("Id") ON DELETE CASCADE
);

-- Seed data for the Questions table
INSERT INTO "Questions" ("Id", "Text", "IsActive")
VALUES
    (1, 'In what city were you born?', 1),
    (2, 'What is the name of your favorite pet?', 1),
    (3, 'What is your mother''s maiden name?', 1),
    (4, 'What high school did you attend?', 1),
    (5, 'What was the mascot of your high school?', 1),
    (6, 'What was the make of your first car?', 1),
    (7, 'What was your favorite toy as a child?', 1),
    (8, 'Where did you meet your spouse?', 1),
    (9, 'What is your favorite meal?', 1),
    (10, 'Who is your favorite actor / actress?', 1),
    (11, 'What is your favorite album?', 1);

