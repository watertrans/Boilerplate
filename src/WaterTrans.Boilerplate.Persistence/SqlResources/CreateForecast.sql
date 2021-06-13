CREATE TABLE IF NOT EXISTS `Forecast` (
    `ForecastId` CHAR(36) NOT NULL
  , `ForecastCode` VARCHAR(20) NOT NULL
  , `CountryCode` VARCHAR(3) NOT NULL
  , `CityCode` VARCHAR(3) NOT NULL
  , `Date` DATE NOT NULL
  , `Temperature` INT NOT NULL
  , `Summary` VARCHAR(100) NOT NULL
  , `CreateTime` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
  , `UpdateTime` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
  , `ConcurrencyToken` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
  , PRIMARY KEY (`ForecastId`)
);

CREATE UNIQUE INDEX IF NOT EXISTS `ix_Forecast_ForecastCode` ON `Forecast` (`ForecastCode`);
CREATE INDEX IF NOT EXISTS `ix_Forecast_CountryCode` ON `Forecast` (`CountryCode`);
CREATE INDEX IF NOT EXISTS `ix_Forecast_CityCode` ON `Forecast` (`CityCode`);
