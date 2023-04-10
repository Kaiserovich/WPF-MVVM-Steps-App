## **WPF-App “Step Analysis”**

C# WPF application using MVVM pattern to analyze the number of steps taken over a certain period (with graphing) for different users. The LiveChart library is used to build the chart.

The table of users contains the following fields:

- Information about the user (Name and Surname);
- The average number of steps taken over the entire period;
- The best result for the entire period;
- The worst result for the entire period.

The graph for the selected user shows the dependence of the steps taken by day.

The application accepts files in JSON format

Export of data by the selected user to the disk (you can choose from: XML, JSON, CSV). Fields Rank and Status that are not displayed in the interface are also exported. Also saved values from the table: the average number of steps, the best/worst result.

The maximum or minimum number of steps on the graph are highlighted in red

## WPF-приложение “Анализатор шагов”

C# WPF приложение с применением паттерна MVVM для возможности анализа количества пройденных шагов за определенный период (с построением графика) по разным пользователям. Для построении графика используется библиотека LiveChart. 

Таблица пользователей содержит в себе следующие поля:

- Информация о пользователе (Фамилия и имя);
- Среднее количество пройденных шагов за весь период;
- Лучший результат за весь период;
- Худший результат за весь период.

График по выбранному пользователю отображает зависимость пройденных шагов по дням.

Для работы приложение принимает файлы в JSON-формате

Реализован экспорт данных по выбранному пользователю на диск (на выбор: XML, JSON, CSV). Поля Rank и Status, которые не отображаются в интерфейсе, тоже экспортируются. Также сохраняться значения из таблицы: среднее количество пройденных шагов, лучший/худший результат.

Максимальное или минимальное количество шагов на графике выделены красным цветом 

### Формат данных

Файлы содержат в себе данные в JSON-формате следующего вида:

[{
"Rank": 5,
"User": "Сидоров Виктор",
"Status": "Finished",
"Steps": 4325
},

{

"Rank": 6,                                       // Рейтинг пользователя за текущий день

"User": "Иванова Марина",          // Имя пользователя

"Status": "Finished",                       // Статус (завершил, отказался и др.)

"Steps": 7560                                  // Количество пройденных шагов

}]

![Screenshot_4](https://user-images.githubusercontent.com/31707173/220577758-a3009e06-edbd-42d3-bb60-243f94ac86e3.png)
