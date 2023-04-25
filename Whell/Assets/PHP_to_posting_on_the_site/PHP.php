<?php
if ($_SERVER['REQUEST_METHOD'] === 'POST'){

    $userID = $_POST["userID"];
    $balanceOpiration = $_POST["balanceOpiration"];
    $Type = $_POST["Type"];
    
    $mysqli = new mysqli("localhost", "root", "", "test1");


    //on error
    if ($mysqli->connect_errno) {
        echo "Ошибка подключения к MySQL: " . $mysqli->connect_error;
        exit();
    }
    
    $res = $mysqli->query("INSERT INTO money (userID, amount, Type) VALUES ($userID, $balanceOpiration, $Type)");
        

    if ($res) {
        echo "Данные успешно добавлены в базу данных.";
    } else {
        echo "Ошибка записи данных в базу данных: " . $mysqli->error;
    }
}


// Проверяем метод запроса
else if ($_SERVER['REQUEST_METHOD'] === 'GET') {
    $ID = $_GET["ID"];

    $mysqli = new mysqli("localhost", "root", "", "test1");

    $res = $mysqli->query("SELECT SUM(amount) AS balance FROM money WHERE UserID = $ID");

// обработка результатов запроса
while ($row = $res->fetch_assoc()) {
    echo $row["balance"];
}
}
?>
