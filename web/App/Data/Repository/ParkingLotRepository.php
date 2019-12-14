<?php

namespace App\Data\Repository;

use App\Data\Model\ParkingLot;
use Framework\Data\Repository\SqlRepository;
use Framework\Persistence\Sql\Driver\SqlDriver;
use Framework\Persistence\Sql\QueryBuilder\Mapper\IQueryBuilderMapper;
use Framework\Persistence\Sql\QueryBuilder\QueryBuilderFactory;
use Framework\Persistence\Sql\QueryBuilderOperator;
use PDO;

class ParkingLotRepository extends SqlRepository
{
    private QueryBuilderFactory $factory;
    private IQueryBuilderMapper $mapper;

    public function __construct(SqlDriver $driver, QueryBuilderFactory $factory, IQueryBuilderMapper $mapper)
    {
        parent::__construct($driver);
        $this->factory = $factory;
        $this->mapper = $mapper;
    }

    /**
     * @param int $id
     * @return ParkingLot
     */
    public function get(int $id): ParkingLot
    {
        $select = $this->factory->Select("parkingLots", "p")
            ->select(["p.*"])
            ->where("p.id", QueryBuilderOperator::EQUAL, ":id");
        $query = $this->mapper->MapSelect($select);
        $stmt = $this->driver->executeQuery($query, [
            ":id" => $id
        ]);
        /** @var ParkingLot $lot */
        $lot = $stmt->fetchObject(ParkingLot::class);

        return $lot;
    }

    /**
     * @return ParkingLot[]
     */
    public function getAll(): array
    {
        $select = $this->factory->Select("parkingLots", "p")
            ->select(["p.*"]);
        $query = $this->mapper->MapSelect($select);
        $stmt = $this->driver->executeQuery($query, []);
        $stmt->setFetchMode(PDO::FETCH_CLASS, ParkingLot::class);
        /** @var ParkingLot[] $lots */
        $lots = $stmt->fetchAll();

        return $lots;
    }

    /**
     * @param int $id
     * @param int $current
     * @return bool
     */
    public function setCurrent(int $id, int $current): bool
    {
        $update = $this->factory->Update("parkingLots")
            ->update("current", ":current")
            ->where("id", QueryBuilderOperator::EQUAL, ":id");
        $query = $this->mapper->MapUpdate($update);
        $stmt = $this->driver->executeQuery($query, [
            ":id" => $id,
            ":current" => $current
        ]);
        return $stmt->rowCount() > 0;
    }
}