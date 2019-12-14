<?php

use Framework\View\View;

$view = new View('_base.php');
?>

<?php $view->beginSection('title') ?>
Hello, world!
<?php $view->endSection() ?>

<?php $view->beginSection('stylesheets') ?>
<link rel="stylesheet" href="/static/css/bootstrap.css" />
<?php $view->endSection() ?>

<?php $view->beginSection('main') ?>
<img style="display: none;" id="map" src="/static/map.jpg" />
<canvas id="root">
</canvas>
<?php $view->endSection() ?>

<?php $view->beginSection('scripts') ?>
<script src="/static/js/home.js" type="text/javascript"></script>
<?php $view->endSection() ?>
