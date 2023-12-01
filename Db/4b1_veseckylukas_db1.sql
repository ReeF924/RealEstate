-- phpMyAdmin SQL Dump
-- version 4.9.4
-- https://www.phpmyadmin.net/
--
-- Host: localhost
-- Generation Time: Dec 03, 2023 at 01:50 PM
-- Server version: 10.3.25-MariaDB-0+deb10u1
-- PHP Version: 5.6.36-0+deb8u1

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `4b1_veseckylukas_db1`
--

-- --------------------------------------------------------

--
-- Table structure for table `tbAdmins`
--

CREATE TABLE `tbAdmins` (
  `Id` int(11) NOT NULL,
  `Type` tinyint(1) NOT NULL,
  `Username` varchar(50) COLLATE utf8_czech_ci NOT NULL,
  `Password` longtext COLLATE utf8_czech_ci NOT NULL,
  `Name` varchar(30) COLLATE utf8_czech_ci NOT NULL,
  `Surname` varchar(30) COLLATE utf8_czech_ci NOT NULL,
  `Email` varchar(50) COLLATE utf8_czech_ci NOT NULL,
  `PhoneNumber` varchar(15) COLLATE utf8_czech_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_czech_ci;

--
-- Dumping data for table `tbAdmins`
--

INSERT INTO `tbAdmins` (`Id`, `Type`, `Username`, `Password`, `Name`, `Surname`, `Email`, `PhoneNumber`) VALUES
(1, 1, 'jirka', 'sral', 'Jirka', 'Sral', 'jirka.sral@email.com', '420602231215'),
(2, 0, 'ondy', 'Sibrava', 'Ondra', 'Sibrava', 'ondry.siby@email.com', '420602231215'),
(3, 0, 'huntethan', 'missimpo', 'Ethan', 'Hunt', 'TomCruise@miss.impo', '420 420 696 969'),
(4, 0, 'hutchJosh', 'hungergames', 'Josh', 'Hutcherson', 'hunger@games.tw', '420612231215'),
(5, 0, 'cagenicholas', 'foobar', 'Nicholas', 'Cage', 'wannabe@badass.us', '224111222333'),
(6, 1, 'midget', 'slovak', 'Marcel', 'Gadzik', 'gadzik@midget.sk', '212211222444');

-- --------------------------------------------------------

--
-- Table structure for table `tbFavouriteOffers`
--

CREATE TABLE `tbFavouriteOffers` (
  `IdUser` int(11) NOT NULL,
  `IdOffer` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_czech_ci;

-- --------------------------------------------------------

--
-- Table structure for table `tbImages`
--

CREATE TABLE `tbImages` (
  `Id` int(11) NOT NULL,
  `IdOffer` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_czech_ci;

--
-- Dumping data for table `tbImages`
--

INSERT INTO `tbImages` (`Id`, `IdOffer`) VALUES
(3, 2),
(4, 2),
(5, 2),
(6, 2);

-- --------------------------------------------------------

--
-- Table structure for table `tbInquiries`
--

CREATE TABLE `tbInquiries` (
  `Id` int(11) NOT NULL,
  `IdOffer` int(11) NOT NULL,
  `IdUser` int(11) DEFAULT NULL,
  `Name` varchar(50) COLLATE utf8_czech_ci NOT NULL,
  `PhoneNumber` varchar(200) COLLATE utf8_czech_ci NOT NULL,
  `Email` varchar(60) COLLATE utf8_czech_ci NOT NULL,
  `AdditionalInformation` varchar(200) COLLATE utf8_czech_ci DEFAULT NULL,
  `DateTimeSent` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_czech_ci;

-- --------------------------------------------------------

--
-- Table structure for table `tbOfferParameters`
--

CREATE TABLE `tbOfferParameters` (
  `Id` int(11) NOT NULL,
  `IdParameter` int(11) NOT NULL,
  `IdOffer` int(11) NOT NULL,
  `Value` varchar(50) COLLATE utf8_czech_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_czech_ci;

--
-- Dumping data for table `tbOfferParameters`
--

INSERT INTO `tbOfferParameters` (`Id`, `IdParameter`, `IdOffer`, `Value`) VALUES
(1, 1, 2, 'panelová'),
(2, 2, 2, 'po rekonstrukci'),
(3, 3, 2, 'Osobní'),
(4, 4, 2, '41m3'),
(5, 5, 2, '7 podlaží'),
(6, 6, 2, '3. NP'),
(7, 7, 2, 'částečně zařízený'),
(8, 8, 2, 'ano'),
(9, 9, 2, 'UPC');

-- --------------------------------------------------------

--
-- Table structure for table `tbOffers`
--

CREATE TABLE `tbOffers` (
  `Id` int(11) NOT NULL,
  `IdAdmin` int(11) DEFAULT NULL,
  `Name` varchar(50) COLLATE utf8_czech_ci NOT NULL,
  `Price` int(15) NOT NULL,
  `Location` varchar(50) COLLATE utf8_czech_ci NOT NULL,
  `Description` longtext COLLATE utf8_czech_ci NOT NULL,
  `ShortDescription` varchar(100) COLLATE utf8_czech_ci DEFAULT NULL,
  `Category` char(1) COLLATE utf8_czech_ci NOT NULL,
  `EnergyClass` char(1) COLLATE utf8_czech_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_czech_ci;

--
-- Dumping data for table `tbOffers`
--

INSERT INTO `tbOffers` (`Id`, `IdAdmin`, `Name`, `Price`, `Location`, `Description`, `ShortDescription`, `Category`, `EnergyClass`) VALUES
(1, 1, 'Noob hill', 600001312, 'Chanov', 'Tohle je dum, je to penke hnusnej a dost predrazenej dum', NULL, 'c', 'C'),
(2, 1, 'Franklin\'s House', 600001312, 'Los Santos', 'Random barak, ve kterym musis bydlet z nejakyho duvodu, je k tomu auto, kteryho se nezbavis, gl', 'Bacha bydli tu vostrej n****', 'l', 'A'),
(3, 1, 'Warsaw Pact room', 600001312, 'Warsaw, Poland', 'bacha, jsou to trochu hovada', NULL, 'c', 'B'),
(4, 2, 'Softfare flat', 12312, 'Frenc', 'Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Proin in tellus sit amet nibh dignissim sagittis. Nulla non arcu lacinia neque faucibus fringilla. Ut enim ad minima veniam, quis nostrum exercitationem ullam corporis suscipit laboriosam, nisi ut aliquid ex ea commodi consequatur? Quis autem vel eum iure reprehenderit qui in ea voluptate velit esse quam nihil molestiae consequatur, vel illum qui dolorem eum fugiat quo voluptas nulla pariatur? Integer tempor. Nam libero tempore, cum soluta nobis est eligendi optio cumque nihil impedit quo minus id quod maxime placeat facere possimus, omnis voluptas assumenda est, omnis dolor repellendus. Aenean id metus id velit ullamcorper pulvinar. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos hymenaeos. Nulla non arcu lacinia neque faucibus fringilla. Sed vel lectus. Donec odio tempus molestie, porttitor ut, iaculis quis, sem. Nam sed tellus id magna elementum tincidunt. Temporibus autem quibusdam et aut officiis debitis aut rerum necessitatibus saepe eveniet ut et voluptates repudiandae sint et molestiae non recusandae. Duis pulvinar. Suspendisse nisl. Etiam commodo dui eget wisi. Fusce tellus odio, dapibus id fermentum quis, suscipit id erat. Sed elit dui, pellentesque a, faucibus vel, interdum nec, diam.\r\n\r\nNunc auctor. Proin in tellus sit amet nibh dignissim sagittis. Pellentesque pretium lectus id turpis. Nam quis nulla. Etiam quis quam. Quisque tincidunt scelerisque libero. Nullam sapien sem, ornare ac, nonummy non, lobortis a enim. Etiam quis quam. Duis viverra diam non justo. Nullam sit amet magna in magna gravida vehicula. Morbi leo mi, nonummy eget tristique non, rhoncus non leo. Etiam quis quam. Maecenas ipsum velit, consectetuer eu lobortis ut, dictum at dui. Nam sed tellus id magna elementum tincidunt. Vivamus luctus egestas leo. Sed convallis magna eu sem. Etiam egestas wisi a erat. Nulla quis diam. Sed ac dolor sit amet purus malesuada congue.\r\n', NULL, 'f', 'F');

-- --------------------------------------------------------

--
-- Table structure for table `tbParameters`
--

CREATE TABLE `tbParameters` (
  `Id` int(11) NOT NULL,
  `Value` varchar(30) COLLATE utf8_czech_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_czech_ci;

--
-- Dumping data for table `tbParameters`
--

INSERT INTO `tbParameters` (`Id`, `Value`) VALUES
(1, 'Konstrukce budovy'),
(2, 'Stav Budovy'),
(3, 'Vlastnictví'),
(4, 'Užitná plocha bytu'),
(5, 'Počet podláží budovy'),
(6, 'Podlaží bytu'),
(7, 'Vybavení'),
(8, 'Lodžie'),
(9, 'Připojení k internetu'),
(10, 'Vault');

-- --------------------------------------------------------

--
-- Table structure for table `tbUsers`
--

CREATE TABLE `tbUsers` (
  `Id` int(11) NOT NULL,
  `Username` varchar(50) COLLATE utf8_czech_ci NOT NULL,
  `Password` longtext COLLATE utf8_czech_ci NOT NULL,
  `Name` varchar(20) COLLATE utf8_czech_ci NOT NULL,
  `Surname` varchar(30) COLLATE utf8_czech_ci NOT NULL,
  `Email` varchar(50) COLLATE utf8_czech_ci NOT NULL,
  `TelephoneNumber` varchar(15) COLLATE utf8_czech_ci NOT NULL,
  `ProfilePicturePath` varchar(50) COLLATE utf8_czech_ci DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_czech_ci;

--
-- Indexes for dumped tables
--

--
-- Indexes for table `tbAdmins`
--
ALTER TABLE `tbAdmins`
  ADD PRIMARY KEY (`Id`);

--
-- Indexes for table `tbFavouriteOffers`
--
ALTER TABLE `tbFavouriteOffers`
  ADD PRIMARY KEY (`IdUser`,`IdOffer`),
  ADD KEY `fk_tbFavouriteOffers_IdOffer` (`IdOffer`);

--
-- Indexes for table `tbImages`
--
ALTER TABLE `tbImages`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `fk_tbImages_IdOffer` (`IdOffer`);

--
-- Indexes for table `tbInquiries`
--
ALTER TABLE `tbInquiries`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `fk_tbInquiries_IdOffer` (`IdOffer`),
  ADD KEY `fk_tbInquiries_IdUser` (`IdUser`);

--
-- Indexes for table `tbOfferParameters`
--
ALTER TABLE `tbOfferParameters`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `fk_tbOfferParametres_idOffer` (`IdOffer`),
  ADD KEY `fk_tbOfferParameters_IdParameter` (`IdParameter`);

--
-- Indexes for table `tbOffers`
--
ALTER TABLE `tbOffers`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `fk_tbOffers_IdAdmin` (`IdAdmin`);

--
-- Indexes for table `tbParameters`
--
ALTER TABLE `tbParameters`
  ADD PRIMARY KEY (`Id`);

--
-- Indexes for table `tbUsers`
--
ALTER TABLE `tbUsers`
  ADD PRIMARY KEY (`Id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `tbAdmins`
--
ALTER TABLE `tbAdmins`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT for table `tbImages`
--
ALTER TABLE `tbImages`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT for table `tbInquiries`
--
ALTER TABLE `tbInquiries`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `tbOfferParameters`
--
ALTER TABLE `tbOfferParameters`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;

--
-- AUTO_INCREMENT for table `tbOffers`
--
ALTER TABLE `tbOffers`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT for table `tbParameters`
--
ALTER TABLE `tbParameters`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- AUTO_INCREMENT for table `tbUsers`
--
ALTER TABLE `tbUsers`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `tbFavouriteOffers`
--
ALTER TABLE `tbFavouriteOffers`
  ADD CONSTRAINT `fk_tbFavouriteOffers_IdOffer` FOREIGN KEY (`IdOffer`) REFERENCES `tbOffers` (`Id`) ON DELETE CASCADE ON UPDATE NO ACTION,
  ADD CONSTRAINT `fk_tbFavouriteOffers_IdUser` FOREIGN KEY (`IdUser`) REFERENCES `tbUsers` (`Id`) ON DELETE CASCADE ON UPDATE NO ACTION;

--
-- Constraints for table `tbImages`
--
ALTER TABLE `tbImages`
  ADD CONSTRAINT `fk_tbImages_IdOffer` FOREIGN KEY (`IdOffer`) REFERENCES `tbOffers` (`Id`) ON DELETE CASCADE ON UPDATE NO ACTION;

--
-- Constraints for table `tbInquiries`
--
ALTER TABLE `tbInquiries`
  ADD CONSTRAINT `fk_tbInquiries_IdOffer` FOREIGN KEY (`IdOffer`) REFERENCES `tbOffers` (`Id`) ON DELETE CASCADE ON UPDATE NO ACTION,
  ADD CONSTRAINT `fk_tbInquiries_IdUser` FOREIGN KEY (`IdUser`) REFERENCES `tbUsers` (`Id`) ON DELETE SET NULL ON UPDATE NO ACTION;

--
-- Constraints for table `tbOfferParameters`
--
ALTER TABLE `tbOfferParameters`
  ADD CONSTRAINT `fk_tbOfferParameters_IdParameter` FOREIGN KEY (`IdParameter`) REFERENCES `tbParameters` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `fk_tbOfferParametres_idOffer` FOREIGN KEY (`IdOffer`) REFERENCES `tbOffers` (`Id`) ON DELETE CASCADE ON UPDATE NO ACTION;

--
-- Constraints for table `tbOffers`
--
ALTER TABLE `tbOffers`
  ADD CONSTRAINT `fk_tbOffers_IdAdmin` FOREIGN KEY (`IdAdmin`) REFERENCES `tbAdmins` (`Id`) ON DELETE SET NULL ON UPDATE NO ACTION;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
