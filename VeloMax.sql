# Création de la base de donnée
DROP DATABASE IF EXISTS veloMax; 
CREATE DATABASE IF NOT EXISTS veloMax; 
USE veloMax;
SET sql_safe_updates=0;

--
DROP TABLE IF EXISTS fournisseur;
DROP TABLE IF EXISTS boutique;
DROP TABLE IF EXISTS particulier;
DROP TABLE IF EXISTS piece;
DROP TABLE IF EXISTS programme;
DROP TABLE IF EXISTS assemblage;
DROP TABLE IF EXISTS velo;
DROP TABLE IF EXISTS PappartientA;
DROP TABLE IF EXISTS commande;
DROP TABLE IF EXISTS contientV;
DROP TABLE IF EXISTS livre;
DROP TABLE IF EXISTS Peffectue;
DROP TABLE IF EXISTS Beffectue;
DROP TABLE IF EXISTS contientP;

--
#SELECT user FROM MySql.user;
#Creation des utilisateurs
#DROP USER 'root'@'localhost';
#CREATE USER 'root'@'localhost' IDENTIFIED BY 'root';
#GRANT ALL ON veloMax.* TO 'root'@'localhost';


#DROP USER 'bozo'@'localhost';
#CREATE USER 'bozo'@'localhost' IDENTIFIED BY 'bozo';
#GRANT SELECT ON veloMax.* TO 'bozo'@'localhost';
#REVOKE DELETE ON veloMax.* FROM 'bozo'@'localhost';
--

CREATE TABLE `veloMax`.`adresse`(
	`numAd` INT NOT NULL,
    `nombre` INT NULL,
    `rue` VARCHAR(50) NOT NULL,
    `codePostal` INT NULL CHECK (codePostal <= 99999),
    `ville` VARCHAR(40) NULL,
    `province` VARCHAR(30),
    PRIMARY KEY (`numAd`)
);
CREATE TABLE `veloMax`.`fournisseur` (
  `siret` VARCHAR(15) NOT NULL CHECK(LENGTH(siret)<=14),
  `nomf` VARCHAR(20) NOT NULL,
  `contact` VARCHAR(20) NULL,
  `adresseF` INT NOT NULL,
  `libelle` INT NULL,
  CONSTRAINT AdresseF FOREIGN KEY(`adresseF`)
    REFERENCES `veloMax`.`adresse` (`numAd`)
    ON DELETE CASCADE
    ON UPDATE CASCADE,
  PRIMARY KEY (`siret`) );

  CREATE TABLE `veloMax`.`programme`(
	`numProg` INT NOT NULL,
    `description` VARCHAR(50) NULL,
    `cout` INT NULL,
    `duree` INT NULL,
    `rabais` DECIMAL NULL,
    PRIMARY KEY(`numProg`));

CREATE TABLE `veloMax`.`particulier` (
  `codePa` INT NOT NULL,
  `adresseP` INT NOT NULL,
  `nomPa` VARCHAR(20) NOT NULL,
  `prenom` VARCHAR(20) NULL,
  `telephone` VARCHAR(12) NULL CHECK (LENGTH(telephone) <= 10),
  `courriel` VARCHAR(50) NULL,
  `dateA` DATE NULL,
  `numProg` INT,
  PRIMARY KEY (`codePa`),
  CONSTRAINT AdresseP FOREIGN KEY(`adresseP`)
    REFERENCES `veloMax`.`adresse` (`numAd`)
    ON DELETE CASCADE
    ON UPDATE CASCADE,
  CONSTRAINT CR_ProgrammeParticulier FOREIGN KEY (`numProg`)
		REFERENCES `veloMax`.`programme` (`numProg`)
		ON DELETE CASCADE
		ON UPDATE CASCADE);

CREATE TABLE `veloMax`.`assemblage`(
	`numA` INT NOT NULL,
	`nomA` VARCHAR(30) NOT NULL,
    `grandeur` VARCHAR(30) NULL,
    `cadre` VARCHAR(4) NULL CHECK(LENGTH(cadre)<=4),
    `guidon` VARCHAR(3) NULL CHECK(LENGTH(guidon)<=3),
    `freins` VARCHAR(2) NULL CHECK(LENGTH(freins)<=2),
    `selle` VARCHAR(3) NULL CHECK(LENGTH(selle)<=3),
    `derailleurAv` VARCHAR(5) NULL CHECK(LENGTH(derailleurAv)<=5),
    `derailleurAr` VARCHAR(5) NULL CHECK(LENGTH(derailleurAr)<=5),
    `roueAv` VARCHAR(3) NULL CHECK(LENGTH(roueAv)<=3),
    `roueAr` VARCHAR(3) NULL CHECK(LENGTH(roueAr)<=3),
    `reflecteurs` VARCHAR(3) NULL CHECK(LENGTH(reflecteurs)<=3),
    `pedalier` VARCHAR(3) NULL CHECK(LENGTH(pedalier)<=3),
    `ordinateur` VARCHAR(2) NULL CHECK(LENGTH(ordinateur)<=2),
    `panier` VARCHAR(3) NULL CHECK (LENGTH(panier)<=3),
    PRIMARY KEY (`numA`)
    );


CREATE TABLE `veloMax`.`velo`(
	`numV` INT NOT NULL,
    `nom` VARCHAR(30) NULL,
    `grandeur` VARCHAR(30) NULL,
    `prix` INT NULL,
    `ligne` VARCHAR(30) NULL,
    `dateD` DATE NULL,
    `dateF` DATE NULL,
    `numA` INT NOT NULL,
    `quantitéEnStockVelo` INT NULL, 
    PRIMARY KEY (`numV`),
   CONSTRAINT CR_VeloAssemblage FOREIGN KEY (`numA`)
		REFERENCES `veloMax`.`assemblage` (`numA`)
		ON DELETE CASCADE
		ON UPDATE CASCADE );


CREATE TABLE `veloMax`.`boutique` (
  `nomB` VARCHAR(30) NOT NULL,
  `adresseB` INT NOT NULL,
  `telephone` VARCHAR(12) NULL CHECK (LENGTH(telephone) <= 10),
  `courriel` VARCHAR(50) NULL,
  `contact` VARCHAR(20) NULL,
  `remise` INT NULL CHECK (remise <= 100),
  CONSTRAINT AdresseB FOREIGN KEY(`adresseB`)
    REFERENCES `veloMax`.`adresse` (`numAd`)
    ON DELETE CASCADE
    ON UPDATE CASCADE,
  PRIMARY KEY (`nomB`) );
  
CREATE TABLE `veloMax`.`piece` (
  `numP` VARCHAR(10) NOT NULL,
  `description` VARCHAR(50) NULL,
  `prixU` INT NULL,
  `dateI` DATE NULL,
  `dateD` DATE NULL,
  `quantitéEnStockPiece` INT NULL,
  PRIMARY KEY (`numP`) );
  

CREATE TABLE `veloMax`.`PappartientA` (
	`numA` INT NOT NULL,
    `numP` VARCHAR(10) NOT NULL,
	 CONSTRAINT CR_AssemblagePieces1 FOREIGN KEY (`numA`)
		REFERENCES `veloMax`.`assemblage` (`numA`)
		ON DELETE CASCADE
		ON UPDATE CASCADE,
	 CONSTRAINT CR_AssemblagePieces2 FOREIGN KEY (`numP`)
		REFERENCES `veloMax`.`piece` (`numP`)
		ON DELETE CASCADE
		ON UPDATE CASCADE,
	PRIMARY KEY(`numA`,`numP`)
    );

CREATE TABLE `veloMax`.`livre` (
	`numP` VARCHAR(10) NOT NULL,
    `siret`  VARCHAR(15) NOT NULL,
    `prixf` INT NULL,
	`numPinF` VARCHAR(10) NOT NULL,
    `delai` INT NULL,
    `quantiteP` INT NULL,
    `date` DATE NULL,
	 CONSTRAINT CR_FournisseurPiece1 FOREIGN KEY (`siret`)
		REFERENCES `veloMax`.`fournisseur` (`siret`)
		ON DELETE CASCADE
		ON UPDATE CASCADE,
	 CONSTRAINT CR_FournisseurPiece2 FOREIGN KEY (`numP`)
		REFERENCES `veloMax`.`piece` (`numP`)
		ON DELETE CASCADE
		ON UPDATE CASCADE,
	PRIMARY KEY(`siret`,`numP`)
    );

CREATE TABLE `veloMax`.`commande`(
	`numC` INT UNSIGNED NOT NULL AUTO_INCREMENT,
    `dateC` DATE NULL,
    `adresseL` INT NOT NULL,
    `dateL` DATE NULL,
    `codePa` INT NULL,
    `nomB` VARCHAR(30) NULL,
    CONSTRAINT AdresseL FOREIGN KEY(`adresseL`)
    REFERENCES `veloMax`.`adresse` (`numAd`)
    ON DELETE CASCADE
    ON UPDATE CASCADE,
    CONSTRAINT BoutiqueC FOREIGN KEY(`nomB`)
    REFERENCES `velomax`.`boutique` (`nomB`)
    ON DELETE CASCADE 
    ON UPDATE CASCADE,
    CONSTRAINT ParticulierC FOREIGN KEY (`codePa`)
    REFERENCES `veloMax`.`particulier` (`codePa`)
    ON DELETE CASCADE
    ON UPDATE CASCADE,
    PRIMARY KEY (`numC`)
);

CREATE TABLE `veloMax`.`contientV` (
	 `quantite` INT NOT NULL,
     `numC` INT UNSIGNED NOT NULL AUTO_INCREMENT,
     `numV` INT NOT NULL,
	 CONSTRAINT CR_CommandeVelo1 FOREIGN KEY (`numC`)
		REFERENCES `veloMax`.`commande` (`numC`)
		ON DELETE CASCADE
		ON UPDATE CASCADE,
	 CONSTRAINT CR_CommandeVelo2 FOREIGN KEY (`numV`)
		REFERENCES `veloMax`.`velo` (`numV`)
		ON DELETE CASCADE
		ON UPDATE CASCADE,
	PRIMARY KEY(`numC`,`numV`)
    );
CREATE TABLE `veloMax`.`contientP` (
	 `quantite` INT NOT NULL,
     `numC`  INT UNSIGNED NOT NULL AUTO_INCREMENT,
     `numP` VARCHAR(10) NOT NULL,
	 CONSTRAINT  CR_CommandePiece1 FOREIGN KEY (`numC`)
		REFERENCES `veloMax`.`commande` (`numC`)
		ON DELETE CASCADE
		ON UPDATE CASCADE,
	 CONSTRAINT CR_CommandePiece2 FOREIGN KEY (`numP`)
		REFERENCES `veloMax`.`piece` (`numP`)
		ON DELETE CASCADE
		ON UPDATE CASCADE,
	PRIMARY KEY(`numC`,`numP`)
    );

#SHOW VARIABLES LIKE "secure_file_priv";
#insertion des différents assemblages de vélos
LOAD DATA INFILE 'C:/ProgramData/MySQL/MySQL Server 8.0/Uploads/assemblage.csv' 
    INTO TABLE assemblage
    FIELDS
        TERMINATED BY ';'
    LINES 
        STARTING BY ''   
        TERMINATED BY '\r\n';

#insertion des différents programmes d'adhésion
LOAD DATA INFILE 'C:/ProgramData/MySQL/MySQL Server 8.0/Uploads/programme.csv' 
    INTO TABLE programme
    FIELDS
        TERMINATED BY ';'
    LINES 
        STARTING BY ''   
        TERMINATED BY '\r\n';

#insertion des différents modèles de vélos
LOAD DATA INFILE 'C:/ProgramData/MySQL/MySQL Server 8.0/Uploads/modele.csv' 
    INTO TABLE velo
    FIELDS
        TERMINATED BY ';'
    LINES 
        STARTING BY ''   
        TERMINATED BY '\r\n';

#insertion des adresses
INSERT INTO `veloMax`.`adresse` (`numAd`,`nombre`,`rue`,`codePostal`,`ville`,`province`) VALUES (1,7,'Rue Jules Ferry',95200,'Sarcelles',"Val d'oise");
INSERT INTO `veloMax`.`adresse` (`numAd`,`nombre`,`rue`,`codePostal`,`ville`,`province`) VALUES (2,49,'Avenue des Lilas',75001,'Paris I',"Paris");
INSERT INTO `veloMax`.`adresse` (`numAd`,`nombre`,`rue`,`codePostal`,`ville`,`province`) VALUES (3,100 ,'Boulevard Gambetta',92500,'Rueil-Malmaison',"Hauts de Seine");
INSERT INTO `veloMax`.`adresse` (`numAd`,`nombre`,`rue`,`codePostal`,`ville`,`province`) VALUES (4,22, 'Avenue Grand-champs',94028,'Créteil',"Val de Marne");
INSERT INTO `veloMax`.`adresse` (`numAd`,`nombre`,`rue`,`codePostal`,`ville`,`province`) VALUES (5,4,'Avenue du Mesnil',78400,'Chatou','Yvelines');
INSERT INTO `veloMax`.`adresse` (`numAd`,`nombre`,`rue`,`codePostal`,`ville`,`province`) VALUES (6,18,'Avenue du Général Leclerc',93500,'Pantin','Seine-Saint-Denis');
INSERT INTO `veloMax`.`adresse` (`numAd`,`nombre`,`rue`,`codePostal`,`ville`,`province`) VALUES (7,37, 'Rue des Etats',94110,'Arceueil','Val-de-Marne');
INSERT INTO `veloMax`.`adresse` (`numAd`,`nombre`,`rue`,`codePostal`,`ville`,`province`) VALUES (8,9 ,'Avenue Jaures',92062,'Puteaux','Hauts-de-Seine');
INSERT INTO `veloMax`.`adresse` (`numAd`,`nombre`,`rue`,`codePostal`,`ville`,`province`) VALUES (9,12, 'Rue de la résistance',78005,'Achères','Yvelines');
INSERT INTO `veloMax`.`adresse` (`numAd`,`nombre`,`rue`,`codePostal`,`ville`,`province`) VALUES (10,4,'Avenue Gallilé',78630,'Orgeval','Yvelines');
INSERT INTO `veloMax`.`adresse` (`numAd`,`nombre`,`rue`,`codePostal`,`ville`,`province`) VALUES (11,39,'Rue des ponts',78290,'Croissy','Yvelines');	
INSERT INTO `veloMax`.`adresse` (`numAd`,`nombre`,`rue`,`codePostal`,`ville`,`province`) VALUES (12,2,'Avenue De Gaulle',78400,'Chatou','Yvelines');	
INSERT INTO `veloMax`.`adresse` (`numAd`,`nombre`,`rue`,`codePostal`,`ville`,`province`) VALUES (13,1, 'Rue Pierre',78400,'Chatou','Yvelines');	
INSERT INTO `veloMax`.`adresse` (`numAd`,`nombre`,`rue`,`codePostal`,`ville`,`province`) VALUES (14,7 ,'Avenue de Wailly',78650,'Le Vesinet','Yvelines');	
INSERT INTO `veloMax`.`adresse` (`numAd`,`nombre`,`rue`,`codePostal`,`ville`,`province`) VALUES (15,15,'Rue des lilas',92062,'Puteaux','Hauts-de-Seine');	
INSERT INTO `veloMax`.`adresse` (`numAd`,`nombre`,`rue`,`codePostal`,`ville`,`province`) VALUES (16,38,'Boulevard Richelieu',78400,'Chatou','Yvelines');	
INSERT INTO `veloMax`.`adresse` (`numAd`,`nombre`,`rue`,`codePostal`,`ville`,`province`) VALUES (17,100,'Rue des  Etats',94110,'Arceueil','Val-de-Marne');	

#insertion des fournisseurs
INSERT INTO `veloMax`.`fournisseur` (`siret`,`nomf`,`contact`,`adresseF`,`libelle`) VALUES ('13858279181082','Velo For You','Emma Garcia',1,1);
INSERT INTO `veloMax`.`fournisseur` (`siret`,`nomf`,`contact`,`adresseF`,`libelle`) VALUES ('49601956385029','Expert Bike','Jong Eun',2,3);
INSERT INTO `veloMax`.`fournisseur` (`siret`,`nomf`,`contact`,`adresseF`,`libelle`) VALUES ('71913583752960','Deux roues Premium','Ilyes Kaoud',3,2);
INSERT INTO `veloMax`.`fournisseur` (`siret`,`nomf`,`contact`,`adresseF`,`libelle`) VALUES ('28598206828105','Velo au carre','Elise Dupont',4,4);

#insertion de boutiques
INSERT INTO `veloMax`.`boutique` (`nomB`,`adresseB`,`telephone`,`courriel`,`contact`,`remise`) VALUES ('Velo Occasion',5,'0639582850','velooccas@gmail.com','Jeff Roulet',8);
INSERT INTO `veloMax`.`boutique` (`nomB`,`adresseB`,`telephone`,`courriel`,`contact`,`remise`) VALUES ('NewBike',6,'0739103916','newbike@gmail.com','Alex Bagaut',10);
INSERT INTO `veloMax`.`boutique` (`nomB`,`adresseB`,`telephone`,`courriel`,`contact`,`remise`) VALUES ('Velo at the Best',7,'0947294036','bikeatthebest@gmail.com','Clément Nièvres',12);
INSERT INTO `veloMax`.`boutique` (`nomB`,`adresseB`,`telephone`,`courriel`,`contact`,`remise`) VALUES ('Boutique des cyclistes',8,'0139485716','bdc@gmail.com','Agathe Mune',12);

#insertion de clients particuliers
INSERT INTO `veloMax`.`particulier` (`codePa`,`adresseP`,`nomPa`,`prenom`,`telephone`,`courriel`,`dateA`,`numProg`) VALUES (1,9,'Lejeune','Bastien','0639471640','b.l@gmail.com',STR_TO_DATE('06-25-2020','%m-%d-%Y'),1);
INSERT INTO `veloMax`.`particulier` (`codePa`,`adresseP`,`nomPa`,`prenom`,`telephone`,`courriel`,`dateA`,`numProg`) VALUES (2,10,'Levy','Quentin','0659206827','q.l@gmail.com',STR_TO_DATE('3-10-2020','%m-%d-%Y'),4);
INSERT INTO `veloMax`.`particulier` (`codePa`,`adresseP`,`nomPa`,`prenom`,`telephone`,`courriel`,`dateA`,`numProg`) VALUES (3,11,'Rey','Amelie','0649015830','a.r@gmail.com',STR_TO_DATE('2-20-2018','%m-%d-%Y'),0);
INSERT INTO `veloMax`.`particulier` (`codePa`,`adresseP`,`nomPa`,`prenom`,`telephone`,`courriel`,`dateA`,`numProg`) VALUES (4,17,'Van Eck','Marceau','0643375290','m.ve@gmail.com',STR_TO_DATE('2-20-2019','%m-%d-%Y'),4);


#insertion de la table pièce
INSERT INTO `veloMax`.`piece` (`numP`,`description`,`prixU`,`dateI`,`dateD`,`quantitéEnStockPiece`) VALUES ('C32','cadre',499,STR_TO_DATE('2-20-2016','%m-%d-%Y'),STR_TO_DATE('2-20-2023','%m-%d-%Y'),2);
INSERT INTO `veloMax`.`piece` (`numP`,`description`,`prixU`,`dateI`,`dateD`,`quantitéEnStockPiece`) VALUES ('C34','cadre',599,STR_TO_DATE('2-20-2015','%m-%d-%Y'),STR_TO_DATE('2-20-2025','%m-%d-%Y'),5);
INSERT INTO `veloMax`.`piece` (`numP`,`description`,`prixU`,`dateI`,`dateD`,`quantitéEnStockPiece`) VALUES ('C76','cadre',299,STR_TO_DATE('2-20-2017','%m-%d-%Y'),STR_TO_DATE('2-20-2024','%m-%d-%Y'),1);
INSERT INTO `veloMax`.`piece` (`numP`,`description`,`prixU`,`dateI`,`dateD`,`quantitéEnStockPiece`) VALUES ('C43','cadre',399,STR_TO_DATE('2-20-2018','%m-%d-%Y'),STR_TO_DATE('2-20-2023','%m-%d-%Y'),4);
INSERT INTO `veloMax`.`piece` (`numP`,`description`,`prixU`,`dateI`,`dateD`,`quantitéEnStockPiece`) VALUES ('C44f','cadre',599,STR_TO_DATE('2-20-2019','%m-%d-%Y'),STR_TO_DATE('2-20-2023','%m-%d-%Y'),3);
INSERT INTO `veloMax`.`piece` (`numP`,`description`,`prixU`,`dateI`,`dateD`,`quantitéEnStockPiece`) VALUES ('C43f','cadre',499,STR_TO_DATE('2-20-2013','%m-%d-%Y'),STR_TO_DATE('2-20-2025','%m-%d-%Y'),2);
INSERT INTO `veloMax`.`piece` (`numP`,`description`,`prixU`,`dateI`,`dateD`,`quantitéEnStockPiece`) VALUES ('C01','cadre',599,STR_TO_DATE('2-20-2014','%m-%d-%Y'),STR_TO_DATE('2-20-2024','%m-%d-%Y'),1);
INSERT INTO `veloMax`.`piece` (`numP`,`description`,`prixU`,`dateI`,`dateD`,`quantitéEnStockPiece`) VALUES ('C02','cadre',499,STR_TO_DATE('2-20-2017','%m-%d-%Y'),STR_TO_DATE('2-20-2022','%m-%d-%Y'),5);
INSERT INTO `veloMax`.`piece` (`numP`,`description`,`prixU`,`dateI`,`dateD`,`quantitéEnStockPiece`) VALUES ('C15','cadre',599,STR_TO_DATE('2-20-2016','%m-%d-%Y'),STR_TO_DATE('2-20-2022','%m-%d-%Y'),1);
INSERT INTO `veloMax`.`piece` (`numP`,`description`,`prixU`,`dateI`,`dateD`,`quantitéEnStockPiece`) VALUES ('C87','cadre',299,STR_TO_DATE('2-20-2016','%m-%d-%Y'),STR_TO_DATE('2-20-2022','%m-%d-%Y'),5);
INSERT INTO `veloMax`.`piece` (`numP`,`description`,`prixU`,`dateI`,`dateD`,`quantitéEnStockPiece`) VALUES ('C87f','cadre',299,STR_TO_DATE('2-20-2018','%m-%d-%Y'),STR_TO_DATE('2-20-2022','%m-%d-%Y'),2);
INSERT INTO `veloMax`.`piece` (`numP`,`description`,`prixU`,`dateI`,`dateD`,`quantitéEnStockPiece`) VALUES ('C25','cadre',499,STR_TO_DATE('2-20-2018','%m-%d-%Y'),STR_TO_DATE('2-20-2023','%m-%d-%Y'),1);
INSERT INTO `veloMax`.`piece` (`numP`,`description`,`prixU`,`dateI`,`dateD`,`quantitéEnStockPiece`) VALUES ('C26','cadre',799,STR_TO_DATE('2-20-2018','%m-%d-%Y'),STR_TO_DATE('2-20-2023','%m-%d-%Y'),2);
INSERT INTO `veloMax`.`piece` (`numP`,`description`,`prixU`,`dateI`,`dateD`,`quantitéEnStockPiece`) VALUES ('G7','guidon',29,STR_TO_DATE('2-20-2016','%m-%d-%Y'),STR_TO_DATE('2-20-2025','%m-%d-%Y'),1);
INSERT INTO `veloMax`.`piece` (`numP`,`description`,`prixU`,`dateI`,`dateD`,`quantitéEnStockPiece`) VALUES ('G9','guidon',49,STR_TO_DATE('2-20-2016','%m-%d-%Y'),STR_TO_DATE('2-20-2024','%m-%d-%Y'),3);
INSERT INTO `veloMax`.`piece` (`numP`,`description`,`prixU`,`dateI`,`dateD`,`quantitéEnStockPiece`) VALUES ('G12','guidon',39,STR_TO_DATE('2-20-2015','%m-%d-%Y'),STR_TO_DATE('2-20-2022','%m-%d-%Y'),1);
INSERT INTO `veloMax`.`piece` (`numP`,`description`,`prixU`,`dateI`,`dateD`,`quantitéEnStockPiece`) VALUES ('F3','freins',39,STR_TO_DATE('2-20-2015','%m-%d-%Y'),STR_TO_DATE('2-20-2024','%m-%d-%Y'),2);
INSERT INTO `veloMax`.`piece` (`numP`,`description`,`prixU`,`dateI`,`dateD`,`quantitéEnStockPiece`) VALUES ('F9','freins',19,STR_TO_DATE('2-20-2015','%m-%d-%Y'),STR_TO_DATE('2-20-2023','%m-%d-%Y'),1);
INSERT INTO `veloMax`.`piece` (`numP`,`description`,`prixU`,`dateI`,`dateD`,`quantitéEnStockPiece`) VALUES ('S88','selle',19,STR_TO_DATE('2-20-2016','%m-%d-%Y'),STR_TO_DATE('2-20-2024','%m-%d-%Y'),3);
INSERT INTO `veloMax`.`piece` (`numP`,`description`,`prixU`,`dateI`,`dateD`,`quantitéEnStockPiece`) VALUES ('S37','selle',29,STR_TO_DATE('2-20-2017','%m-%d-%Y'),STR_TO_DATE('2-20-2023','%m-%d-%Y'),1);
INSERT INTO `veloMax`.`piece` (`numP`,`description`,`prixU`,`dateI`,`dateD`,`quantitéEnStockPiece`) VALUES ('S35','selle',15,STR_TO_DATE('2-20-2019','%m-%d-%Y'),STR_TO_DATE('2-20-2024','%m-%d-%Y'),2);
INSERT INTO `veloMax`.`piece` (`numP`,`description`,`prixU`,`dateI`,`dateD`,`quantitéEnStockPiece`) VALUES ('S02','selle',39,STR_TO_DATE('2-20-2018','%m-%d-%Y'),STR_TO_DATE('2-20-2023','%m-%d-%Y'),4);
INSERT INTO `veloMax`.`piece` (`numP`,`description`,`prixU`,`dateI`,`dateD`,`quantitéEnStockPiece`) VALUES ('S03','selle',19,STR_TO_DATE('2-20-2017','%m-%d-%Y'),STR_TO_DATE('2-20-2024','%m-%d-%Y'),1);
INSERT INTO `veloMax`.`piece` (`numP`,`description`,`prixU`,`dateI`,`dateD`,`quantitéEnStockPiece`) VALUES ('S36','selle',29,STR_TO_DATE('2-20-2014','%m-%d-%Y'),STR_TO_DATE('2-20-2024','%m-%d-%Y'),2);
INSERT INTO `veloMax`.`piece` (`numP`,`description`,`prixU`,`dateI`,`dateD`,`quantitéEnStockPiece`) VALUES ('S34','selle',9,STR_TO_DATE('2-20-2017','%m-%d-%Y'),STR_TO_DATE('2-20-2025','%m-%d-%Y'),1);
INSERT INTO `veloMax`.`piece` (`numP`,`description`,`prixU`,`dateI`,`dateD`,`quantitéEnStockPiece`) VALUES ('S87','selle',19,STR_TO_DATE('2-20-2016','%m-%d-%Y'),STR_TO_DATE('2-20-2025','%m-%d-%Y'),2);
INSERT INTO `veloMax`.`piece` (`numP`,`description`,`prixU`,`dateI`,`dateD`,`quantitéEnStockPiece`) VALUES ('DV133','derailleurAv',19,STR_TO_DATE('2-20-2016','%m-%d-%Y'),STR_TO_DATE('2-20-2025','%m-%d-%Y'),1);
INSERT INTO `veloMax`.`piece` (`numP`,`description`,`prixU`,`dateI`,`dateD`,`quantitéEnStockPiece`) VALUES ('DV17','derailleurAv',29,STR_TO_DATE('2-20-2016','%m-%d-%Y'),STR_TO_DATE('2-20-2025','%m-%d-%Y'),1);
INSERT INTO `veloMax`.`piece` (`numP`,`description`,`prixU`,`dateI`,`dateD`,`quantitéEnStockPiece`) VALUES ('DV87','derailleurAv',39,STR_TO_DATE('2-20-2019','%m-%d-%Y'),STR_TO_DATE('2-20-2023','%m-%d-%Y'),3);
INSERT INTO `veloMax`.`piece` (`numP`,`description`,`prixU`,`dateI`,`dateD`,`quantitéEnStockPiece`) VALUES ('DV57','derailleurAv',19,STR_TO_DATE('2-20-2019','%m-%d-%Y'),STR_TO_DATE('2-20-2024','%m-%d-%Y'),0);
INSERT INTO `veloMax`.`piece` (`numP`,`description`,`prixU`,`dateI`,`dateD`,`quantitéEnStockPiece`) VALUES ('DV15','derailleurAv',29,STR_TO_DATE('2-20-2019','%m-%d-%Y'),STR_TO_DATE('2-20-2025','%m-%d-%Y'),1);
INSERT INTO `veloMax`.`piece` (`numP`,`description`,`prixU`,`dateI`,`dateD`,`quantitéEnStockPiece`) VALUES ('DV41','derailleurAv',19,STR_TO_DATE('2-20-2019','%m-%d-%Y'),STR_TO_DATE('2-20-2024','%m-%d-%Y'),0);
INSERT INTO `veloMax`.`piece` (`numP`,`description`,`prixU`,`dateI`,`dateD`,`quantitéEnStockPiece`) VALUES ('DV132','derailleurAv',29,STR_TO_DATE('2-20-2018','%m-%d-%Y'),STR_TO_DATE('2-20-2032','%m-%d-%Y'),2);
INSERT INTO `veloMax`.`piece` (`numP`,`description`,`prixU`,`dateI`,`dateD`,`quantitéEnStockPiece`) VALUES ('DR56','derailleurAr',29,STR_TO_DATE('2-20-2016','%m-%d-%Y'),STR_TO_DATE('2-20-2021','%m-%d-%Y'),5);
INSERT INTO `veloMax`.`piece` (`numP`,`description`,`prixU`,`dateI`,`dateD`,`quantitéEnStockPiece`) VALUES ('DR87','derailleurAr',39,STR_TO_DATE('2-20-2016','%m-%d-%Y'),STR_TO_DATE('2-20-2024','%m-%d-%Y'),0);
INSERT INTO `veloMax`.`piece` (`numP`,`description`,`prixU`,`dateI`,`dateD`,`quantitéEnStockPiece`) VALUES ('DR86','derailleurAr',19,STR_TO_DATE('2-20-2017','%m-%d-%Y'),STR_TO_DATE('2-20-2023','%m-%d-%Y'),2);
INSERT INTO `veloMax`.`piece` (`numP`,`description`,`prixU`,`dateI`,`dateD`,`quantitéEnStockPiece`) VALUES ('DR23','derailleurAr',9,STR_TO_DATE('2-20-2018','%m-%d-%Y'),STR_TO_DATE('2-20-2023','%m-%d-%Y'),1);
INSERT INTO `veloMax`.`piece` (`numP`,`description`,`prixU`,`dateI`,`dateD`,`quantitéEnStockPiece`) VALUES ('DR76','derailleurAr',49,STR_TO_DATE('2-20-2019','%m-%d-%Y'),STR_TO_DATE('2-20-2024','%m-%d-%Y'),1);
INSERT INTO `veloMax`.`piece` (`numP`,`description`,`prixU`,`dateI`,`dateD`,`quantitéEnStockPiece`) VALUES ('DR52','derailleurAr',19,STR_TO_DATE('2-20-2020','%m-%d-%Y'),STR_TO_DATE('2-20-2024','%m-%d-%Y'),1);
INSERT INTO `veloMax`.`piece` (`numP`,`description`,`prixU`,`dateI`,`dateD`,`quantitéEnStockPiece`) VALUES ('R45','roueAv',19,STR_TO_DATE('2-20-2020','%m-%d-%Y'),STR_TO_DATE('2-20-2025','%m-%d-%Y'),1);
INSERT INTO `veloMax`.`piece` (`numP`,`description`,`prixU`,`dateI`,`dateD`,`quantitéEnStockPiece`) VALUES ('R48','roueAv',29,STR_TO_DATE('2-20-2018','%m-%d-%Y'),STR_TO_DATE('2-20-2025','%m-%d-%Y'),4);
INSERT INTO `veloMax`.`piece` (`numP`,`description`,`prixU`,`dateI`,`dateD`,`quantitéEnStockPiece`) VALUES ('R19','roueAv',29,STR_TO_DATE('2-20-2019','%m-%d-%Y'),STR_TO_DATE('2-20-2023','%m-%d-%Y'),3);
INSERT INTO `veloMax`.`piece` (`numP`,`description`,`prixU`,`dateI`,`dateD`,`quantitéEnStockPiece`) VALUES ('R1','roueAv',29,STR_TO_DATE('2-20-2018','%m-%d-%Y'),STR_TO_DATE('2-20-2023','%m-%d-%Y'),2);
INSERT INTO `veloMax`.`piece` (`numP`,`description`,`prixU`,`dateI`,`dateD`,`quantitéEnStockPiece`) VALUES ('R11','roueAv',39,STR_TO_DATE('2-20-2017','%m-%d-%Y'),STR_TO_DATE('2-20-2023','%m-%d-%Y'),1);
INSERT INTO `veloMax`.`piece` (`numP`,`description`,`prixU`,`dateI`,`dateD`,`quantitéEnStockPiece`) VALUES ('R44','roueAv',29,STR_TO_DATE('2-20-2020','%m-%d-%Y'),STR_TO_DATE('2-20-2023','%m-%d-%Y'),1);
INSERT INTO `veloMax`.`piece` (`numP`,`description`,`prixU`,`dateI`,`dateD`,`quantitéEnStockPiece`) VALUES ('R46','roueAr',29,STR_TO_DATE('2-20-2016','%m-%d-%Y'),STR_TO_DATE('2-20-2021','%m-%d-%Y'),6);
INSERT INTO `veloMax`.`piece` (`numP`,`description`,`prixU`,`dateI`,`dateD`,`quantitéEnStockPiece`) VALUES ('R47','roueAr',39,STR_TO_DATE('2-20-2016','%m-%d-%Y'),STR_TO_DATE('2-20-2022','%m-%d-%Y'),2);
INSERT INTO `veloMax`.`piece` (`numP`,`description`,`prixU`,`dateI`,`dateD`,`quantitéEnStockPiece`) VALUES ('R32','roueAr',29,STR_TO_DATE('2-20-2017','%m-%d-%Y'),STR_TO_DATE('2-20-2025','%m-%d-%Y'),3);
INSERT INTO `veloMax`.`piece` (`numP`,`description`,`prixU`,`dateI`,`dateD`,`quantitéEnStockPiece`) VALUES ('R18','roueAr',19,STR_TO_DATE('2-20-2019','%m-%d-%Y'),STR_TO_DATE('2-20-2024','%m-%d-%Y'),4);
INSERT INTO `veloMax`.`piece` (`numP`,`description`,`prixU`,`dateI`,`dateD`,`quantitéEnStockPiece`) VALUES ('R12','roueAr',29,STR_TO_DATE('2-20-2018','%m-%d-%Y'),STR_TO_DATE('2-20-2024','%m-%d-%Y'),7);
INSERT INTO `veloMax`.`piece` (`numP`,`description`,`prixU`,`dateI`,`dateD`,`quantitéEnStockPiece`) VALUES ('R2','roueAr',39,STR_TO_DATE('2-20-2018','%m-%d-%Y'),STR_TO_DATE('2-20-2024','%m-%d-%Y'),2);
INSERT INTO `veloMax`.`piece` (`numP`,`description`,`prixU`,`dateI`,`dateD`,`quantitéEnStockPiece`) VALUES ('R02','reflecteurs',9,STR_TO_DATE('2-20-2016','%m-%d-%Y'),STR_TO_DATE('2-20-2023','%m-%d-%Y'),3);
INSERT INTO `veloMax`.`piece` (`numP`,`description`,`prixU`,`dateI`,`dateD`,`quantitéEnStockPiece`) VALUES ('R09','reflecteurs',5,STR_TO_DATE('2-20-2016','%m-%d-%Y'),STR_TO_DATE('2-20-2023','%m-%d-%Y'),1);
INSERT INTO `veloMax`.`piece` (`numP`,`description`,`prixU`,`dateI`,`dateD`,`quantitéEnStockPiece`) VALUES ('R10','reflecteurs',19,STR_TO_DATE('2-20-2016','%m-%d-%Y'),STR_TO_DATE('2-20-2022','%m-%d-%Y'),3);
INSERT INTO `veloMax`.`piece` (`numP`,`description`,`prixU`,`dateI`,`dateD`,`quantitéEnStockPiece`) VALUES ('P12','pedalier',59,STR_TO_DATE('2-20-2017','%m-%d-%Y'),STR_TO_DATE('2-20-2022','%m-%d-%Y'),2);
INSERT INTO `veloMax`.`piece` (`numP`,`description`,`prixU`,`dateI`,`dateD`,`quantitéEnStockPiece`) VALUES ('P34','pedalier',39,STR_TO_DATE('2-20-2017','%m-%d-%Y'),STR_TO_DATE('2-20-2023','%m-%d-%Y'),4);
INSERT INTO `veloMax`.`piece` (`numP`,`description`,`prixU`,`dateI`,`dateD`,`quantitéEnStockPiece`) VALUES ('P1','pedalier',29,STR_TO_DATE('2-20-2017','%m-%d-%Y'),STR_TO_DATE('2-20-2021','%m-%d-%Y'),0);
INSERT INTO `veloMax`.`piece` (`numP`,`description`,`prixU`,`dateI`,`dateD`,`quantitéEnStockPiece`) VALUES ('P15','reflecteurs',39,STR_TO_DATE('2-20-2017','%m-%d-%Y'),STR_TO_DATE('2-20-2021','%m-%d-%Y'),1);
INSERT INTO `veloMax`.`piece` (`numP`,`description`,`prixU`,`dateI`,`dateD`,`quantitéEnStockPiece`) VALUES ('O2','ordinateur',49,STR_TO_DATE('2-20-2018','%m-%d-%Y'),STR_TO_DATE('2-20-2021','%m-%d-%Y'),5);
INSERT INTO `veloMax`.`piece` (`numP`,`description`,`prixU`,`dateI`,`dateD`,`quantitéEnStockPiece`) VALUES ('O4','ordinateur',29,STR_TO_DATE('2-20-2018','%m-%d-%Y'),STR_TO_DATE('2-20-2024','%m-%d-%Y'),3);
INSERT INTO `veloMax`.`piece` (`numP`,`description`,`prixU`,`dateI`,`dateD`,`quantitéEnStockPiece`) VALUES ('S01','panier',19,STR_TO_DATE('2-20-2018','%m-%d-%Y'),STR_TO_DATE('2-20-2023','%m-%d-%Y'),0);
INSERT INTO `veloMax`.`piece` (`numP`,`description`,`prixU`,`dateI`,`dateD`,`quantitéEnStockPiece`) VALUES ('S05','panier',19,STR_TO_DATE('2-20-2019','%m-%d-%Y'),STR_TO_DATE('2-20-2022','%m-%d-%Y'),6);
INSERT INTO `veloMax`.`piece` (`numP`,`description`,`prixU`,`dateI`,`dateD`,`quantitéEnStockPiece`) VALUES ('S74','panier',39,STR_TO_DATE('2-20-2019','%m-%d-%Y'),STR_TO_DATE('2-20-2022','%m-%d-%Y'),5);
INSERT INTO `veloMax`.`piece` (`numP`,`description`,`prixU`,`dateI`,`dateD`,`quantitéEnStockPiece`) VALUES ('S73','panier',29,STR_TO_DATE('2-20-2016','%m-%d-%Y'),STR_TO_DATE('2-20-2022','%m-%d-%Y'),2);


#insertion de la table livre
INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('R09','13858279181082',4,'12R09',4,1,'2021-02-21');
INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('R09','71913583752960',3,'71R09',12,5,'2021-01-21');

INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('C43f','71913583752960',490,'71R09',5,7,'2021-02-21');
INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('C43f','28598206828105',481,'28R09',10,3,'2021-01-21');
INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('C43f','49601956385029',495,'R09',4,3,'2021-03-21');

INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('F3','49601956385029',37,'49F3',6,5,'2021-02-21');
INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('F3','13858279181082',30,'13F3',13,7,'2021-03-21');

INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('S03','71913583752960',17,'71S05',6,2,'2021-02-21');
INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('S03','28598206828105',18,'28S05',5,2,'2021-04-21');
INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('S03','49601956385029',17,'49S05',8,2,'2021-03-21');

INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('R02','13858279181082',35,'13R2',7,5,'2021-02-21');
INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('R02','71913583752960',36,'71R2',5,5,'2021-01-21');

INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('DR23','49601956385029',8,'49DR',3,5,'2021-02-21');
INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('DR23','28598206828105',7,'28DR',6,4,'2021-03-21');

INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('C34','13858279181082',590,'13C3',8,5,'2021-02-21');
INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('C34','49601956385029',595,'49C3',4,3,'2021-01-21');

INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('O4','28598206828105',24,'28O4',9,3,'2021-01-21');
INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('O4','49601956385029',26,'49O4',5,4,'2021-04-21');
INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('O4','71913583752960',28,'71O4',2,5,'2021-03-21');

INSERT INTO `veloMax`.`livre` (`numP`,`siret`,`prixf`,`numPinF`,`delai`,`quantiteP`,`date`) VALUES ('C32','71913583752960',120,'C1',10,4,'2021-03-21');
INSERT INTO `veloMax`.`livre` (`numP`,`siret`,`prixf`,`numPinF`,`delai`,`quantiteP`,`date`) VALUES ('C32','13858279181082',100,'C10',5,4,'2021-01-21');

INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('G7','13858279181082',30,'G7bis',4,3,'2021-02-01');
INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('G7','28598206828105',50,'G10',6,2,'2021-01-21');

INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('S88','13858279181082',30,'G7bis',4,3,'2021-02-01');
INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('S88','28598206828105',50,'G10',6,2,'2021-01-21');

INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('DV133','28598206828105',45,'D2',3,1,'2021-01-11');

INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('C76','13858279181082',250,'12R09',4,1,'2021-02-21');
INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('C76','71913583752960',300,'71R09',12,5,'2021-01-21');

INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('C43','71913583752960',490,'71R09',5,7,'2021-02-21');
INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('C43','28598206828105',481,'28R09',10,3,'2021-01-21');
INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('C43','49601956385029',495,'R09',4,3,'2021-03-21');

INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('C44f','49601956385029',370,'49F3',6,5,'2021-02-21');
INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('C44f','13858279181082',300,'13F3',13,7,'2021-03-21');

INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('C01','71913583752960',170,'71S05',6,2,'2021-02-21');
INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('C01','28598206828105',180,'28S05',5,2,'2021-04-21');
INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('C01','49601956385029',170,'49S05',8,2,'2021-03-21');

INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('C02','13858279181082',350,'13R2',7,5,'2021-02-21');
INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('C02','71913583752960',360,'71R2',5,5,'2021-01-21');

INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('C15','49601956385029',80,'49DR',3,5,'2021-02-21');
INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('C15','28598206828105',70,'28DR',6,4,'2021-03-21');

INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('C87','13858279181082',590,'13C3',8,5,'2021-02-21');
INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('C87','49601956385029',595,'49C3',4,3,'2021-01-21');

INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('C87f','28598206828105',240,'28O4',9,3,'2021-01-21');
INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('C87f','49601956385029',260,'49O4',5,4,'2021-04-21');
INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('C87f','71913583752960',280,'71O4',2,5,'2021-03-21');

INSERT INTO `veloMax`.`livre` (`numP`,`siret`,`prixf`,`numPinF`,`delai`,`quantiteP`,`date`) VALUES ('C25','71913583752960',120,'C1',10,4,'2021-03-21');
INSERT INTO `veloMax`.`livre` (`numP`,`siret`,`prixf`,`numPinF`,`delai`,`quantiteP`,`date`) VALUES ('C25','13858279181082',100,'C10',5,4,'2021-01-21');

INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('C26','13858279181082',300,'G7bis',4,3,'2021-02-01');
INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('C26','28598206828105',500,'G10',6,2,'2021-01-21');

INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('G9','13858279181082',30,'G7bis',4,3,'2021-02-01');
INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('G9','28598206828105',50,'G10',6,2,'2021-01-21');

INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('G12','28598206828105',50,'G10',6,2,'2021-01-21');
INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('G12','13858279181082',40,'12R09',4,1,'2021-02-21');
INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('G12','71913583752960',30,'71R09',12,5,'2021-01-21');

INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('F9','71913583752960',50,'71R09',5,7,'2021-02-21');
INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('F9','28598206828105',45,'28R09',10,3,'2021-01-21');
INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('F9','49601956385029',35,'R09',4,3,'2021-03-21');

INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('S37','49601956385029',36,'49F3',6,5,'2021-02-21');
INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('S37','13858279181082',30,'13F3',13,7,'2021-03-21');

INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('S35','71913583752960',17,'71S05',6,2,'2021-02-21');
INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('S35','28598206828105',18,'28S05',5,2,'2021-04-21');
INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('S35','49601956385029',17,'49S05',8,2,'2021-03-21');

INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('S02','13858279181082',35,'13R2',7,5,'2021-02-21');
INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('S02','71913583752960',36,'71R2',5,5,'2021-01-21');

INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('S36','49601956385029',28,'49DR',3,5,'2021-02-21');
INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('S36','28598206828105',27,'28DR',6,4,'2021-03-21');

INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('S34','13858279181082',590,'13C3',8,5,'2021-02-21');
INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('S34','49601956385029',595,'49C3',4,3,'2021-01-21');

INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('S87','28598206828105',24,'28O4',9,3,'2021-01-21');
INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('S87','49601956385029',26,'49O4',5,4,'2021-04-21');
INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('S87','71913583752960',28,'71O4',2,5,'2021-03-21');

INSERT INTO `veloMax`.`livre` (`numP`,`siret`,`prixf`,`numPinF`,`delai`,`quantiteP`,`date`) VALUES ('DV17','71913583752960',120,'C1',10,4,'2021-03-21');
INSERT INTO `veloMax`.`livre` (`numP`,`siret`,`prixf`,`numPinF`,`delai`,`quantiteP`,`date`) VALUES ('DV17','13858279181082',100,'C10',5,4,'2021-01-21');

INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('DV87','13858279181082',30,'G7bis',4,3,'2021-02-01');
INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('DV87','28598206828105',50,'G10',6,2,'2021-01-21');

INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('DV57','13858279181082',30,'G7bis',4,3,'2021-02-01');
INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('DV57','28598206828105',50,'G10',6,2,'2021-01-21');

INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('DV15','28598206828105',50,'G10',6,2,'2021-01-21');
INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('DV15','13858279181082',40,'12R09',4,1,'2021-02-21');
INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('DV15','71913583752960',30,'71R09',12,5,'2021-01-21');

INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('DV41','71913583752960',49,'71R09',5,7,'2021-02-21');
INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('DV41','28598206828105',48,'28R09',10,3,'2021-01-21');
INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('DV41','49601956385029',49,'R09',4,3,'2021-03-21');

INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('DV132','49601956385029',37,'49F3',6,5,'2021-02-21');
INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('DV132','13858279181082',30,'13F3',13,7,'2021-03-21');

INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('DR56','71913583752960',17,'71S05',6,2,'2021-02-21');
INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('DR56','28598206828105',18,'28S05',5,2,'2021-04-21');
INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('DR56','49601956385029',17,'49S05',8,2,'2021-03-21');

INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('DR87','13858279181082',35,'13R2',7,5,'2021-02-21');
INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('DR87','71913583752960',36,'71R2',5,5,'2021-01-21');

INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('DR86','49601956385029',8,'49DR',3,5,'2021-02-21');
INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('DR86','28598206828105',7,'28DR',6,4,'2021-03-21');

INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('DR76','13858279181082',59,'13C3',8,5,'2021-02-21');
INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('DR76','49601956385029',55,'49C3',4,3,'2021-01-21');

INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('DR52','28598206828105',24,'28O4',9,3,'2021-01-21');
INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('DR52','49601956385029',26,'49O4',5,4,'2021-04-21');
INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('DR52','71913583752960',28,'71O4',2,5,'2021-03-21');

INSERT INTO `veloMax`.`livre` (`numP`,`siret`,`prixf`,`numPinF`,`delai`,`quantiteP`,`date`) VALUES ('R45','71913583752960',32,'C1',10,4,'2021-03-21');
INSERT INTO `veloMax`.`livre` (`numP`,`siret`,`prixf`,`numPinF`,`delai`,`quantiteP`,`date`) VALUES ('R45','13858279181082',20,'C10',5,4,'2021-01-21');

INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('R48','13858279181082',30,'G7bis',4,3,'2021-02-01');
INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('R48','28598206828105',50,'G10',6,2,'2021-01-21');

INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('R12','13858279181082',30,'G7bis',4,3,'2021-02-01');
INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('R12','28598206828105',50,'G10',6,2,'2021-01-21');

INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('R19','28598206828105',50,'G10',6,2,'2021-01-21');
INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('R19','13858279181082',40,'12R09',4,1,'2021-02-21');
INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('R19','71913583752960',30,'71R09',12,5,'2021-01-21');

INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('R1','71913583752960',49,'71R09',5,7,'2021-02-21');
INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('R1','28598206828105',48,'28R09',10,3,'2021-01-21');
INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('R1','49601956385029',49,'R09',4,3,'2021-03-21');

INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('R11','49601956385029',37,'49F3',6,5,'2021-02-21');
INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('R11','13858279181082',30,'13F3',13,7,'2021-03-21');

INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('R44','71913583752960',17,'71S05',6,2,'2021-02-21');
INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('R44','28598206828105',18,'28S05',5,2,'2021-04-21');
INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('R44','49601956385029',17,'49S05',8,2,'2021-03-21');

INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('R47','13858279181082',35,'13R2',7,5,'2021-02-21');
INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('R47','71913583752960',36,'71R2',5,5,'2021-01-21');

INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('R18','28598206828105',24,'28O4',9,3,'2021-01-21');
INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('R18','49601956385029',26,'49O4',5,4,'2021-04-21');
INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('R18','71913583752960',28,'71O4',2,5,'2021-03-21');

INSERT INTO `veloMax`.`livre` (`numP`,`siret`,`prixf`,`numPinF`,`delai`,`quantiteP`,`date`) VALUES ('R32','71913583752960',22,'C1',10,4,'2021-03-21');
INSERT INTO `veloMax`.`livre` (`numP`,`siret`,`prixf`,`numPinF`,`delai`,`quantiteP`,`date`) VALUES ('R32','13858279181082',20,'C10',5,4,'2021-01-21');


INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('R46','13858279181082',30,'G7bis',4,3,'2021-02-01');
INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('R46','28598206828105',50,'G10',6,2,'2021-01-21');

INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('R10','28598206828105',50,'G10',6,2,'2021-01-21');
INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('R10','13858279181082',40,'12R09',4,1,'2021-02-21');
INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('R10','71913583752960',30,'71R09',12,5,'2021-01-21');

INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('P12','71913583752960',49,'71R09',5,7,'2021-02-21');
INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('P12','28598206828105',41,'28R09',10,3,'2021-01-21');
INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('P12','49601956385029',45,'R09',4,3,'2021-03-21');

INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('P34','49601956385029',37,'49F3',6,5,'2021-02-21');
INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('P34','13858279181082',30,'13F3',13,7,'2021-03-21');

INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('P1','71913583752960',17,'71S05',6,2,'2021-02-21');
INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('P1','28598206828105',18,'28S05',5,2,'2021-04-21');
INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('P1','49601956385029',17,'49S05',8,2,'2021-03-21');

INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('P15','13858279181082',35,'13R2',7,5,'2021-02-21');
INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('P15','71913583752960',36,'71R2',5,5,'2021-01-21');

INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('O2','49601956385029',80,'49DR',3,5,'2021-02-21');
INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('O2','28598206828105',70,'28DR',6,4,'2021-03-21');

INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('S01','13858279181082',9,'13C3',8,5,'2021-02-21');
INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('S01','49601956385029',10,'49C3',4,3,'2021-01-21');

INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('S05','28598206828105',24,'28O4',9,3,'2021-01-21');
INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('S05','49601956385029',26,'49O4',5,4,'2021-04-21');
INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('S05','71913583752960',28,'71O4',2,5,'2021-03-21');

INSERT INTO `veloMax`.`livre` (`numP`,`siret`,`prixf`,`numPinF`,`delai`,`quantiteP`,`date`) VALUES ('S74','71913583752960',12,'C1',10,4,'2021-03-21');
INSERT INTO `veloMax`.`livre` (`numP`,`siret`,`prixf`,`numPinF`,`delai`,`quantiteP`,`date`) VALUES ('S74','13858279181082',10,'C10',5,4,'2021-01-21');

INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('S73','13858279181082',30,'G7bis',4,3,'2021-02-01');
INSERT INTO veloMax.`livre` (numP,`siret`,`prixf`,`numPinF` ,`delai`,`quantiteP`,`date`) VALUES ('S73','28598206828105',50,'G10',6,2,'2021-01-21');


#insertion des commandes
INSERT INTO `veloMax`.`commande` (`numC`,`dateC`,`adresseL`,`dateL`,`codePa`,`nomB`) VALUES(6,'2021-01-07',13,'2021-01-19',1,null);
INSERT INTO `veloMax`.`contientp`(`numC`,`numP`,`quantite`) VALUES (6,'C32',2);
INSERT INTO `veloMax`.`contientp`(`numC`,`numP`,`quantite`) VALUES (6,'C34',1);

INSERT INTO `veloMax`.`commande` (`numC`,`dateC`,`adresseL`,`dateL`,`codePa`,`nomB`) VALUES(2,'2021-02-28',12,'2021-03-04',null,'Velo Occasion');
INSERT INTO `veloMax`.`contientv`(`quantite`,`numC`,`numV`) VALUES (3,2,101);

INSERT INTO `veloMax`.`commande` (`numC`,`dateC`,`adresseL`,`dateL`,`codePa`,`nomB`) VALUES(3,'2021-03-22',15,'2021-03-31',null,'Boutique des cyclistes');
INSERT INTO `veloMax`.`contientv`(`quantite`,`numC`,`numV`) VALUES (1,3,105);
INSERT INTO `veloMax`.`contientv`(`quantite`,`numC`,`numV`) VALUES (2,3,106);

INSERT INTO `veloMax`.`commande`(`numC`,`dateC`,`adresseL`,`dateL`,`codePa`,`nomB`) VALUES(4,'2021-04-03',16,'2021-04-16',3,null);
INSERT INTO `veloMax`.`contientp`(`numC`,`numP`,`quantite`) VALUES (4,'S74',2);
INSERT INTO `veloMax`.`contientp`(`numC`,`numP`,`quantite`) VALUES (4,'C32',2);
INSERT INTO `veloMax`.`contientv`(`quantite`,`numC`,`numV`) VALUES (1,4,103);
INSERT INTO `veloMax`.`contientv`(`quantite`,`numC`,`numV`) VALUES (2,4,107);

INSERT INTO `veloMax`.`commande`(`numC`,`dateC`,`adresseL`,`dateL`,`codePa`,`nomB`) VALUES(5,'2021-03-27',14,'2021-04-01',null,'Velo at the Best');
INSERT INTO `veloMax`.`contientp`(`numC`,`numP`,`quantite`) VALUES (5,'O2',1);
INSERT INTO `veloMax`.`contientv`(`quantite`,`numC`,`numV`) VALUES (5,5,113);

INSERT INTO `veloMax`.`commande`(`numC`,`dateC`,`adresseL`,`dateL`,`codePa`,`nomB`) VALUES(7,'2021-03-16',14,'2021-04-1',null,'Velo at the Best');
INSERT INTO `veloMax`.`contientp`(`numC`,`numP`,`quantite`) VALUES (7,'C32',1);
INSERT INTO `veloMax`.`contientv`(`quantite`,`numC`,`numV`) VALUES (10,7,111);

INSERT INTO `veloMax`.`commande`(`numC`,`dateC`,`adresseL`,`dateL`,`codePa`,`nomB`) VALUES(8,'2021-02-12',14,'2021-02-21',4,null);
INSERT INTO `veloMax`.`contientv`(`quantite`,`numC`,`numV`) VALUES (1,8,105);


INSERT INTO `veloMax`.`commande`(`numC`,`dateC`,`adresseL`,`dateL`,`codePa`,`nomB`) VALUES(1,'2019-04-17',1,'2019-05-01',null,null);
INSERT INTO `veloMax`.`contientv`(`quantite`,`numC`,`numV`) VALUES (0,1,101);
INSERT INTO `veloMax`.`contientv`(`quantite`,`numC`,`numV`) VALUES (0,1,102);
INSERT INTO `veloMax`.`contientv`(`quantite`,`numC`,`numV`) VALUES (0,1,103);
INSERT INTO `veloMax`.`contientv`(`quantite`,`numC`,`numV`) VALUES (0,1,104);
INSERT INTO `veloMax`.`contientv`(`quantite`,`numC`,`numV`) VALUES (0,1,105);
INSERT INTO `veloMax`.`contientv`(`quantite`,`numC`,`numV`) VALUES (0,1,106);
INSERT INTO `veloMax`.`contientv`(`quantite`,`numC`,`numV`) VALUES (0,1,107);
INSERT INTO `veloMax`.`contientv`(`quantite`,`numC`,`numV`) VALUES (0,1,108);
INSERT INTO `veloMax`.`contientv`(`quantite`,`numC`,`numV`) VALUES (0,1,109);
INSERT INTO `veloMax`.`contientv`(`quantite`,`numC`,`numV`) VALUES (0,1,110);
INSERT INTO `veloMax`.`contientv`(`quantite`,`numC`,`numV`) VALUES (0,1,111);
INSERT INTO `veloMax`.`contientv`(`quantite`,`numC`,`numV`) VALUES (0,1,112);
INSERT INTO `veloMax`.`contientv`(`quantite`,`numC`,`numV`) VALUES (0,1,113);
INSERT INTO `veloMax`.`contientv`(`quantite`,`numC`,`numV`) VALUES (0,1,114);
INSERT INTO `veloMax`.`contientv`(`quantite`,`numC`,`numV`) VALUES (0,1,115);
INSERT INTO `veloMax`.`contientp`(`numC`,`numP`,`quantite`) VALUES (1,'C01',0);
INSERT INTO `veloMax`.`contientp`(`numC`,`numP`,`quantite`) VALUES (1,'C02',0);
INSERT INTO `veloMax`.`contientp`(`numC`,`numP`,`quantite`) VALUES (1,'G12',0);
INSERT INTO `veloMax`.`contientp`(`numC`,`numP`,`quantite`) VALUES (1,'C15',0);
INSERT INTO `veloMax`.`contientp`(`numC`,`numP`,`quantite`) VALUES (1,'C25',0);
INSERT INTO `veloMax`.`contientp`(`numC`,`numP`,`quantite`) VALUES (1,'C26',0);
INSERT INTO `veloMax`.`contientp`(`numC`,`numP`,`quantite`) VALUES (1,'C32',0);
INSERT INTO `veloMax`.`contientp`(`numC`,`numP`,`quantite`) VALUES (1,'C34',0);
INSERT INTO `veloMax`.`contientp`(`numC`,`numP`,`quantite`) VALUES (1,'C43',0);
INSERT INTO `veloMax`.`contientp`(`numC`,`numP`,`quantite`) VALUES (1,'C43f',0);
INSERT INTO `veloMax`.`contientp`(`numC`,`numP`,`quantite`) VALUES (1,'C44f',0);
INSERT INTO `veloMax`.`contientp`(`numC`,`numP`,`quantite`) VALUES (1,'C76',0);
INSERT INTO `veloMax`.`contientp`(`numC`,`numP`,`quantite`) VALUES (1,'C87',0);
INSERT INTO `veloMax`.`contientp`(`numC`,`numP`,`quantite`) VALUES (1,'C87f',0);
INSERT INTO `veloMax`.`contientp`(`numC`,`numP`,`quantite`) VALUES (1,'DR23',0);
INSERT INTO `veloMax`.`contientp`(`numC`,`numP`,`quantite`) VALUES (1,'DR52',0);
INSERT INTO `veloMax`.`contientp`(`numC`,`numP`,`quantite`) VALUES (1,'DR56',0);
INSERT INTO `veloMax`.`contientp`(`numC`,`numP`,`quantite`) VALUES (1,'DR76',0);
INSERT INTO `veloMax`.`contientp`(`numC`,`numP`,`quantite`) VALUES (1,'DR86',0);
INSERT INTO `veloMax`.`contientp`(`numC`,`numP`,`quantite`) VALUES (1,'DR87',0);
INSERT INTO `veloMax`.`contientp`(`numC`,`numP`,`quantite`) VALUES (1,'DV132',0);
INSERT INTO `veloMax`.`contientp`(`numC`,`numP`,`quantite`) VALUES (1,'DV133',0);
INSERT INTO `veloMax`.`contientp`(`numC`,`numP`,`quantite`) VALUES (1,'DV15',0);
INSERT INTO `veloMax`.`contientp`(`numC`,`numP`,`quantite`) VALUES (1,'DV17',0);
INSERT INTO `veloMax`.`contientp`(`numC`,`numP`,`quantite`) VALUES (1,'DV41',0);
INSERT INTO `veloMax`.`contientp`(`numC`,`numP`,`quantite`) VALUES (1,'DV57',0);
INSERT INTO `veloMax`.`contientp`(`numC`,`numP`,`quantite`) VALUES (1,'DV87',0);
INSERT INTO `veloMax`.`contientp`(`numC`,`numP`,`quantite`) VALUES (1,'F3',0);
INSERT INTO `veloMax`.`contientp`(`numC`,`numP`,`quantite`) VALUES (1,'F9',0);
INSERT INTO `veloMax`.`contientp`(`numC`,`numP`,`quantite`) VALUES (1,'G7',0);
INSERT INTO `veloMax`.`contientp`(`numC`,`numP`,`quantite`) VALUES (1,'G9',0);
INSERT INTO `veloMax`.`contientp`(`numC`,`numP`,`quantite`) VALUES (1,'O4',0);
INSERT INTO `veloMax`.`contientp`(`numC`,`numP`,`quantite`) VALUES (1,'P1',0);
INSERT INTO `veloMax`.`contientp`(`numC`,`numP`,`quantite`) VALUES (01,'P12',0);
INSERT INTO `veloMax`.`contientp`(`numC`,`numP`,`quantite`) VALUES (01,'P15',0);
INSERT INTO `veloMax`.`contientp`(`numC`,`numP`,`quantite`) VALUES (1,'P34',0);
INSERT INTO `veloMax`.`contientp`(`numC`,`numP`,`quantite`) VALUES (1,'R02',0);
INSERT INTO `veloMax`.`contientp`(`numC`,`numP`,`quantite`) VALUES (1,'R09',0);
INSERT INTO `veloMax`.`contientp`(`numC`,`numP`,`quantite`) VALUES (1,'R1',0);
INSERT INTO `veloMax`.`contientp`(`numC`,`numP`,`quantite`) VALUES (1,'R10',0);
INSERT INTO `veloMax`.`contientp`(`numC`,`numP`,`quantite`) VALUES (1,'R11',0);
INSERT INTO `veloMax`.`contientp`(`numC`,`numP`,`quantite`) VALUES (1,'R12',0);
INSERT INTO `veloMax`.`contientp`(`numC`,`numP`,`quantite`) VALUES (1,'R18',0);
INSERT INTO `veloMax`.`contientp`(`numC`,`numP`,`quantite`) VALUES (1,'R19',0);
INSERT INTO `veloMax`.`contientp`(`numC`,`numP`,`quantite`) VALUES (01,'R2',0);
INSERT INTO `veloMax`.`contientp`(`numC`,`numP`,`quantite`) VALUES (1,'R32',0);
INSERT INTO `veloMax`.`contientp`(`numC`,`numP`,`quantite`) VALUES (1,'R44',0);
INSERT INTO `veloMax`.`contientp`(`numC`,`numP`,`quantite`) VALUES (1,'R45',0);
INSERT INTO `veloMax`.`contientp`(`numC`,`numP`,`quantite`) VALUES (1,'R46',0);
INSERT INTO `veloMax`.`contientp`(`numC`,`numP`,`quantite`) VALUES (1,'R47',0);
INSERT INTO `veloMax`.`contientp`(`numC`,`numP`,`quantite`) VALUES (1,'R48',0);
INSERT INTO `veloMax`.`contientp`(`numC`,`numP`,`quantite`) VALUES (1,'S01',0);
INSERT INTO `veloMax`.`contientp`(`numC`,`numP`,`quantite`) VALUES (1,'S02',0);
INSERT INTO `veloMax`.`contientp`(`numC`,`numP`,`quantite`) VALUES (1,'S03',0);
INSERT INTO `veloMax`.`contientp`(`numC`,`numP`,`quantite`) VALUES (1,'S05',0);
INSERT INTO `veloMax`.`contientp`(`numC`,`numP`,`quantite`) VALUES (1,'S34',0);
INSERT INTO `veloMax`.`contientp`(`numC`,`numP`,`quantite`) VALUES (1,'S35',0);
INSERT INTO `veloMax`.`contientp`(`numC`,`numP`,`quantite`) VALUES (1,'S36',0);
INSERT INTO `veloMax`.`contientp`(`numC`,`numP`,`quantite`) VALUES (1,'S37',0);
INSERT INTO `veloMax`.`contientp`(`numC`,`numP`,`quantite`) VALUES (1,'S73',0);
INSERT INTO `veloMax`.`contientp`(`numC`,`numP`,`quantite`) VALUES (1,'S74',0);
INSERT INTO `veloMax`.`contientp`(`numC`,`numP`,`quantite`) VALUES (1,'S87',0);
INSERT INTO `veloMax`.`contientp`(`numC`,`numP`,`quantite`) VALUES (1,'S88',0);
INSERT INTO `veloMax`.`contientp`(`numC`,`numP`,`quantite`) VALUES (1,'O2',0);
