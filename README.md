MYSQL

USE paup_vjezbe_2024;
CREATE TABLE ovlasti (
  sifra VARCHAR(5) NOT NULL,
  naziv VARCHAR(255) NOT NULL,
  PRIMARY KEY (sifra)
)


CREATE TABLE korisnici (
  korisnicko_ime VARCHAR(30) NOT NULL,
  email VARCHAR(50) NOT NULL,
  prezime VARCHAR(255) NOT NULL,
  ime VARCHAR(255) NOT NULL,
  lozinka VARCHAR(255) NOT NULL,
  ovlast VARCHAR(5) DEFAULT NULL,
  PRIMARY KEY (korisnicko_ime)
)

ALTER TABLE korisnici 
  ADD CONSTRAINT FK_korisnici_ovlast FOREIGN KEY (ovlast)
    REFERENCES ovlasti(sifra) ON DELETE NO ACTION;


INSERT INTO ovlasti(sifra, naziv) VALUES
('AD', 'Administrator'),
('MO', 'Moderator');


INSERT INTO korisnici(korisnicko_ime,email,prezime,ime,lozinka,ovlast) 
	VALUES('admin','admin@net.hr','Hren','Filip',
		'jUPY60RIRBTWGhhlm0Q/v+UjmVENpGidU1K9ljHGxRs=','AD'),
	('pivanic','pivanic@net.hr','Ivanić','Petar',
		'9OGS0TpjNkgD0+dwSB1lpnsrlAZhsobZwZ5cQEtMOPo=','MO');



Tablica todolists

CREATE todolists (
Id int NOT NULL AUTO_INCREMENT, 
Name VARCHAR(255) NOT NULL,
korisnickoIme VARCHAR(255) NOT NULL,
PRIMARY KEY (Id) );

Tablica tasks
REATE TABLE Tasks ( Id INT NOT NULL AUTO_INCREMENT, Title VARCHAR(255), DESCRIPTION TEXT, IsCompleted BIT NOT NULL, DueDate DATETIME, TodoListId INT NOT NULL, PRIMARY KEY (Id), CONSTRAINT FK_Tasks_TodoLists FOREIGN KEY (TodoListId) REFERENCES TodoLists(Id) );

