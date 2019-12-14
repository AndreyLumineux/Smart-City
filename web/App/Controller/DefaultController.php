<?php

namespace App\Controller;

use Framework\Controller\Controller;
use Framework\Utils;

class DefaultController extends Controller
{
    public function __construct()
    {
    }
    
    public function homeAction()
    {
        return $this->renderView('home.php');
    }

    public function notFoundAction()
    {
        return $this->renderHtml(Utils::Dump($_REQUEST, false));
    }
}