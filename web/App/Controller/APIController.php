<?php

namespace App\Controller;

use App\Data\Repository\ParkingLotRepository;
use Framework\Controller\Controller;

class APIController extends Controller
{
    private ParkingLotRepository $parkingLotRepository;

    public function __construct(ParkingLotRepository $parkingLotRepository)
    {
        $this->parkingLotRepository = $parkingLotRepository;
    }

    public function getAllParkingLotsAction()
    {
        $all = $this->parkingLotRepository->getAll();
        return $this->renderJson(json_encode($all, JSON_PRETTY_PRINT));
    }

    public function getParkingLotAction(int $id)
    {
        $lot = $this->parkingLotRepository->get($id);
        return $this->renderJson(json_encode($lot, JSON_PRETTY_PRINT));
    }

    public function updateParkingLotAction(int $id, int $current)
    {
        $success = $this->parkingLotRepository->setCurrent($id, $current);
        if ($success) {
            return $this->renderHttpStatusPage(200);
        } else {
            return $this->renderHttpStatusPage(500);
        }
    }
}