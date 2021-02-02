/*La Base de datos es ISO815*/

CREATE TABLE `Employees` (
  `AccountNumber` int(11) NOT NULL,
  `DocumentType` char(1) DEFAULT NULL,
  `DocumentNumber` varchar(45) DEFAULT NULL,
  `Amount` decimal(18,2) DEFAULT NULL,
  PRIMARY KEY (`AccountNumber`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
