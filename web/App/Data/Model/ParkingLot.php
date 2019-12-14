<?php

namespace App\Data\Model;

use Framework\Data\Model\Entity;
use JsonSerializable;

class ParkingLot extends Entity implements JsonSerializable
{
    private int $coord_x;

    private int $coord_y;

    private int $current;

    private int $total_capacity;

    /**
     * @return int
     */
    public function getCoordX(): int
    {
        return $this->coord_x;
    }

    /**
     * @return int
     */
    public function getCoordY(): int
    {
        return $this->coord_y;
    }

    /**
     * @return int
     */
    public function getCurrent()
    {
        return $this->current;
    }

    /**
     * @param int $current
     */
    public function setCurrent($current)
    {
        $this->current = $current;
    }

    /**
     * @return int
     */
    public function getTotalCapacity()
    {
        return $this->total_capacity;
    }

    /**
     * @inheritDoc
     */
    public function jsonSerialize()
    {
        return [
            'id' => $this->getId(),
            'coord_x' => $this->getCoordX(),
            'coord_y' => $this->getCoordY(),
            'current' => $this->getCurrent(),
            'total_capacity' => $this->getTotalCapacity(),
        ];
    }
}
