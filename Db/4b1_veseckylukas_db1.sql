-- phpMyAdmin SQL Dump
-- version 4.9.4
-- https://www.phpmyadmin.net/
--
-- Host: localhost
-- Generation Time: Dec 01, 2023 at 04:25 PM
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
  `Name` varchar(50) COLLATE utf8_czech_ci NOT NULL,
  `Email` varchar(50) COLLATE utf8_czech_ci NOT NULL,
  `PhoneNumber` varchar(15) COLLATE utf8_czech_ci NOT NULL,
  `ProfilePicturePath` varchar(200) COLLATE utf8_czech_ci DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_czech_ci;

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
  `IdOffer` int(11) NOT NULL,
  `ImagePath` varchar(200) COLLATE utf8_czech_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_czech_ci;

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
-- Table structure for table `tbLabels`
--

CREATE TABLE `tbLabels` (
  `Id` int(11) NOT NULL,
  `Value` varchar(20) COLLATE utf8_czech_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_czech_ci;

-- --------------------------------------------------------

--
-- Table structure for table `tbOfferParameters`
--

CREATE TABLE `tbOfferParameters` (
  `Id` int(11) NOT NULL,
  `IdLabel` int(11) NOT NULL,
  `IdOffer` int(11) NOT NULL,
  `Value` varchar(50) COLLATE utf8_czech_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_czech_ci;

-- --------------------------------------------------------

--
-- Table structure for table `tbOffers`
--

CREATE TABLE `tbOffers` (
  `Id` int(11) NOT NULL,
  `IdSeller` int(11) DEFAULT NULL,
  `Name` varchar(50) COLLATE utf8_czech_ci NOT NULL,
  `Price` int(15) NOT NULL,
  `Location` varchar(50) COLLATE utf8_czech_ci NOT NULL,
  `Description` longtext COLLATE utf8_czech_ci NOT NULL,
  `ShortDescription` varchar(100) COLLATE utf8_czech_ci DEFAULT NULL,
  `Category` char(1) COLLATE utf8_czech_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_czech_ci;

-- --------------------------------------------------------

--
-- Table structure for table `tbUsers`
--

CREATE TABLE `tbUsers` (
  `Id` int(11) NOT NULL,
  `Username` varchar(50) COLLATE utf8_czech_ci NOT NULL,
  `Password` longtext COLLATE utf8_czech_ci NOT NULL,
  `Name` varchar(50) COLLATE utf8_czech_ci NOT NULL,
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
-- Indexes for table `tbLabels`
--
ALTER TABLE `tbLabels`
  ADD PRIMARY KEY (`Id`);

--
-- Indexes for table `tbOfferParameters`
--
ALTER TABLE `tbOfferParameters`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `fk_tbOfferParameters_IdLabel` (`IdLabel`),
  ADD KEY `fk_tbOfferParametres_idOffer` (`IdOffer`);

--
-- Indexes for table `tbOffers`
--
ALTER TABLE `tbOffers`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `fk_tbOffers_IdSeller` (`IdSeller`);

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
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `tbImages`
--
ALTER TABLE `tbImages`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `tbInquiries`
--
ALTER TABLE `tbInquiries`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `tbLabels`
--
ALTER TABLE `tbLabels`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `tbOfferParameters`
--
ALTER TABLE `tbOfferParameters`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `tbOffers`
--
ALTER TABLE `tbOffers`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

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
  ADD CONSTRAINT `fk_tbOfferParameters_IdLabel` FOREIGN KEY (`IdLabel`) REFERENCES `tbLabels` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `fk_tbOfferParametres_idOffer` FOREIGN KEY (`IdOffer`) REFERENCES `tbOffers` (`Id`) ON DELETE CASCADE ON UPDATE NO ACTION;

--
-- Constraints for table `tbOffers`
--
ALTER TABLE `tbOffers`
  ADD CONSTRAINT `fk_tbOffers_IdSeller` FOREIGN KEY (`IdSeller`) REFERENCES `tbAdmins` (`Id`) ON DELETE SET NULL ON UPDATE NO ACTION;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
