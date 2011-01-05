-- phpMyAdmin SQL Dump
-- version 3.3.2deb1
-- http://www.phpmyadmin.net
--
-- Хост: localhost
-- Время создания: Ноя 16 2010 г., 16:12
-- Версия сервера: 5.1.41
-- Версия PHP: 5.3.2-1ubuntu4

SET SQL_MODE="NO_AUTO_VALUE_ON_ZERO";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- База данных: `artiblog`
--

-- --------------------------------------------------------

--
-- Структура таблицы `category`
--

CREATE TABLE IF NOT EXISTS `category` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(200) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM  DEFAULT CHARSET=utf8 AUTO_INCREMENT=4 ;

--
-- Дамп данных таблицы `category`
--

INSERT INTO `category` (`id`, `name`) VALUES
(1, 'Программирование'),
(2, 'Юзабилити'),
(3, 'L&W');

-- --------------------------------------------------------

--
-- Структура таблицы `comment`
--

CREATE TABLE IF NOT EXISTS `comment` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `id_post` int(11) NOT NULL,
  `name` varchar(200) NOT NULL,
  `texts` text NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=6 ;

--
-- Дамп данных таблицы `comment`
--

INSERT INTO `comment` (`id`, `id_post`, `name`, `texts`) VALUES
(1, 1, 'backdoor', 'тестирование комментария'),
(2, 1, 'nick', 'ываываыва'),
(3, 1, 'backdoor', 'уре поперло'),
(4, 3, 'nick', 'dfgdfg'),
(5, 3, 'backdoor ', 'я прекрасен');

-- --------------------------------------------------------

--
-- Структура таблицы `post`
--

CREATE TABLE IF NOT EXISTS `post` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `title` varchar(200) NOT NULL,
  `texts` text NOT NULL,
  `date` varchar(200) NOT NULL,
  `category` int(11) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=4 ;

--
-- Дамп данных таблицы `post`
--

INSERT INTO `post` (`id`, `title`, `texts`, `date`, `category`) VALUES
(1, 'Первый пост', 'ура товарищи ура ура \nапрапрапр\nапрапрапрап\nрапрапрпар\nапрапрапрапрпар\nапрапрпарапрапрапр\nапрапрапрапрапр\nапрапрапрпарапр\nапрапрапрапр\n[] \nЗдесь идет текст который должен быть дальше ХЕХЕ', '1 Января 2011', 1),
(2, 'Юзабилити и IE', '<p>Каждый раз когда пишеш UI ,все время почему то забываеш о самой хреновой части этой программы а именно о IE []</p>\r\n<p>IE это такая хуйня с которой возится проще говоря ты не то что заебешся ты охуееш просто впринципе </p>\r\n<p>Потому что это полная ЖОПА</p>', '2 Января 2010', 2),
(3, 'В чем плюсы Linux перед тем же Windows ', '<p>Представьте себе   у вас чистый комп  на нем нет операционной системы, вы нашли из под полы диск с Windows ставите,проходит 30 минут чистая система стоит,и так что дальше? Правильно установка драйверов\\по\\игр и тд. </p>[]\n<p>Вы успешно  установили  все выше сказанное общее потраченное время  составляет 3 часа,ну все вроде бы норм  но косяк на самом деле может случится в любое время в том же Linux такого никогда нет не будет,потому что вы  загребетесь  настраивать Linux  с  начала,но когда вы его настроите  он у вас будет работать как часы,и я думаю вы даже  забудете   про Windows навсегда</p>', '12 Ноября', 3);
