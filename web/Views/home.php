<?php

use Framework\View\View;

$view = new View('_base.php');
?>

<?php $view->beginSection('title') ?>
Hello, world!
<?php $view->endSection() ?>

<?php $view->beginSection('stylesheets') ?>
<link rel="stylesheet" href="/static/css/bootstrap.css"/>
<link rel="stylesheet" href="/static/css/home.css"/>
<?php $view->endSection() ?>

<?php $view->beginSection('main') ?>
<div style="display: none">
    <img id="map" src="/static/images/map.jpg"/>
    <img id="map_pin" src="/static/images/pin.png" />
</div>
<canvas id="root">
</canvas>
<?php $view->endSection() ?>

<?php $view->beginSection('scripts') ?>
<script src="/static/js/home.js" type="text/javascript"></script>
<?php $view->endSection() ?>
