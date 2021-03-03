CREATE TABLE `PAYMENTS` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `TipoDocumento` varchar(1) NOT NULL DEFAULT 'C',
  `NumeroDocumento` varchar(15) NOT NULL,
  `Monto` decimal(18,2) NOT NULL,
  `TipoPago` varchar(1) NOT NULL,
  `Matricula` varchar(10) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4;
