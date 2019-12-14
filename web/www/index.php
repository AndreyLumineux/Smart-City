<?php
require __DIR__ . '/../App/bootstrap.php';

use Framework\App;
use Framework\Logging\FileLogger;
use Framework\Logging\Logger;
use Framework\Persistence\Sql\Driver\GenericSqlDriver;
use Framework\Persistence\Sql\Driver\PostgresDriver;
use Framework\Persistence\Sql\QueryBuilder\Mapper\Postgres\PostgresQueryBuilderMapper;
use Framework\Utils;

$app = App::GetInstance();
setcookie(session_name(), session_id(), time() + 60 * 60 * 24 * 30, '/', '.example.com', true, true);
$app->configureFrom(__DIR__ . '/../config.json');

$logger = new FileLogger(__DIR__ . '/../logs/site.log');
$error_handler = function (int $log_severity, int $http_code, ...$args) use ($logger) {
    if (App::isDevelopment()) {
        Utils::Dump($args);
    } else {
        $logger->log($log_severity, $args);
    }
};

register_shutdown_function(function () use ($error_handler) {
    $error = error_get_last();
    if ($error !== null && $error['type'] === E_ERROR) {
        $error_handler(Logger::LOG_FATAL, 500, $error);
    }
});
$app->registerExceptionHandler(function (Throwable $throwable) use ($error_handler) {
    $error_handler(Logger::LOG_ERROR, 503, [$throwable->getMessage(), $throwable->getTraceAsString()]);
});


$connection = $app->getDatabaseConnections()['default'];
$sql_driver = new GenericSqlDriver($connection);
$query_mapper = new PostgresQueryBuilderMapper();
$app->getDependencyInjectionService()
    ->register($connection)
    ->register($sql_driver)
    ->register($query_mapper)
    ->register($logger);
$app->run();
