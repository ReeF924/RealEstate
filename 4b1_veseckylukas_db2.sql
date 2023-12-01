-- phpMyAdmin SQL Dump
-- version 4.9.4
-- https://www.phpmyadmin.net/
--
-- Host: localhost
-- Generation Time: Nov 30, 2023 at 08:51 PM
-- Server version: 10.3.25-MariaDB-0+deb10u1
-- PHP Version: 5.6.36-0+deb8u1

SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `4b1_veseckylukas_db2`
--

-- --------------------------------------------------------

--
-- Table structure for table `tbAdmins`
--

CREATE TABLE `tbAdmins` (
  `Id` int(11) NOT NULL,
  `Type` tinyint(1) DEFAULT NULL,
  `Username` varchar(50) DEFAULT NULL,
  `Password` longtext DEFAULT NULL,
  `Name` varchar(50) DEFAULT NULL,
  `Email` varchar(50) DEFAULT NULL,
  `PhoneNumber` varchar(15) DEFAULT NULL,
  `ProfilePicturePath` varchar(200) DEFAULT NULL
) ;

-- --------------------------------------------------------

--
-- Table structure for table `tbFavouriteOffers`
--

CREATE TABLE `tbFavouriteOffers` (
  `IdUser` int(11) NOT NULL,
  `IdOffer` int(11) NOT NULL
) ;

-- --------------------------------------------------------

--
-- Table structure for table `tbImages`
--

CREATE TABLE `tbImages` (
  `Id` int(11) NOT NULL,
  `IdOffer` int(11) DEFAULT NULL,
  `ImagePath` varchar(200) DEFAULT NULL
) ;

-- --------------------------------------------------------

--
-- Table structure for table `tbInquiries`
--

CREATE TABLE `tbInquiries` (
  `Id` int(11) NOT NULL,
  `IdSeller` int(11) DEFAULT NULL,
  `IdOffer` int(11) DEFAULT NULL,
  `IdUser` int(11) DEFAULT NULL,
  `Status` tinyint(1) DEFAULT NULL,
  `Name` varchar(50) DEFAULT NULL,
  `TelNumber` varchar(15) DEFAULT NULL,
  `Email` varchar(50) DEFAULT NULL,
  `Text` longtext DEFAULT NULL,
  `DateTimeSent` datetime NOT NULL
) ;

-- --------------------------------------------------------

--
-- Table structure for table `tbLabels`
--

CREATE TABLE `tbLabels` (
  `Id` int(11) NOT NULL,
  `Value` varchar(20) NOT NULL
) ;

-- --------------------------------------------------------

--
-- Table structure for table `tbOfferParameters`
--

CREATE TABLE `tbOfferParameters` (
  `Id` int(11) NOT NULL,
  `IdLabel` int(11) NOT NULL,
  `IdOffer` int(11) NOT NULL,
  `Value` varchar(50) DEFAULT NULL
) ;

-- --------------------------------------------------------

--
-- Table structure for table `tbOffers`
--

CREATE TABLE `tbOffers` (
  `Id` int(11) NOT NULL,
  `IdSeller` int(11) DEFAULT NULL,
  `Name` varchar(50) DEFAULT NULL,
  `Price` int(15) DEFAULT NULL,
  `Location` varchar(50) DEFAULT NULL,
  `Description` longtext DEFAULT NULL,
  `ShortDescription` varchar(100) NOT NULL,
  `Category` char(1) DEFAULT NULL
) ;

-- --------------------------------------------------------

--
-- Table structure for table `tbUsers`
--

CREATE TABLE `tbUsers` (
  `Id` int(11) NOT NULL,
  `Username` varchar(50) NOT NULL,
  `Password` longtext NOT NULL,
  `Name` varchar(50) NOT NULL,
  `Email` varchar(50) NOT NULL,
  `TelephoneNumber` varchar(15) NOT NULL,
  `ProfilePicturePath` varchar(50) DEFAULT NULL
) ;

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
  ADD KEY `fk_tbInquiries_IdSeller` (`IdSeller`),
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
  ADD KEY `fd_tbOfferParameters_IdLabel` (`IdLabel`),
  ADD KEY `fk_tbOfferParameters_IdOffer` (`IdOffer`);

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
-- Constraints for dumped tables
--

--
-- Constraints for table `tbFavouriteOffers`
--
ALTER TABLE `tbFavouriteOffers`
  ADD CONSTRAINT `fk_tbFavouriteOffers_IdOffer` FOREIGN KEY (`IdOffer`) REFERENCES `tbOffers` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `fk_tbFavouriteOffers_IdUser` FOREIGN KEY (`IdUser`) REFERENCES `tbUsers` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Constraints for table `tbImages`
--
ALTER TABLE `tbImages`
  ADD CONSTRAINT `fk_tbImages_IdOffer` FOREIGN KEY (`IdOffer`) REFERENCES `tbOffers` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Constraints for table `tbInquiries`
--
ALTER TABLE `tbInquiries`
  ADD CONSTRAINT `fk_tbInquiries_IdOffer` FOREIGN KEY (`IdOffer`) REFERENCES `tbOffers` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `fk_tbInquiries_IdSeller` FOREIGN KEY (`IdSeller`) REFERENCES `tbAdmins` (`Id`),
  ADD CONSTRAINT `fk_tbInquiries_IdUser` FOREIGN KEY (`IdUser`) REFERENCES `tbUsers` (`Id`);

--
-- Constraints for table `tbOfferParameters`
--
ALTER TABLE `tbOfferParameters`
  ADD CONSTRAINT `fd_tbOfferParameters_IdLabel` FOREIGN KEY (`IdLabel`) REFERENCES `tbLabels` (`Id`),
  ADD CONSTRAINT `fk_tbOfferParameters_IdOffer` FOREIGN KEY (`IdOffer`) REFERENCES `tbOffers` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Constraints for table `tbOffers`
--
ALTER TABLE `tbOffers`
  ADD CONSTRAINT `fk_tbOffers_IdSeller` FOREIGN KEY (`IdSeller`) REFERENCES `tbAdmins` (`Id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
