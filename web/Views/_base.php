<?php

use Framework\View\View;

$view = new View();
?>
<!doctype html>
<html prefix="og: http://ogp.me/ns#" lang="en">
<head>
    <meta charset="UTF-8"/>
    <meta name="viewport"
          content="width=device-width, user-scalable=no, initial-scale=1.0, maximum-scale=1.0, minimum-scale=1.0"/>
    <meta http-equiv="X-UA-Compatible" content="ie=edge"/>

    <meta property="og:description" content="<?= L::meta_description ?>"/>
    <meta name="description" content="<?= L::meta_description ?>"/>
    <?= $view->defineSection('meta') ?>

    <title><?= $view->defineSection('title') ?></title>

    <?= $view->defineSection('stylesheets') ?>
</head>
<body>
<main>
    <?= $view->defineSection('main', true) ?>
</main>
<?= $view->defineSection('scripts') ?>
</body>
</html>
