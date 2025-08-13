-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 13-08-2025 a las 22:45:09
-- Versión del servidor: 10.4.32-MariaDB
-- Versión de PHP: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `sistema_academico`
--

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `usuarios`
--

CREATE TABLE `usuarios` (
  `id` int(11) NOT NULL,
  `cedula` varchar(20) NOT NULL,
  `nombres` varchar(100) NOT NULL,
  `apellidos` varchar(100) NOT NULL,
  `correo` varchar(100) NOT NULL,
  `contraseña` varchar(64) NOT NULL,
  `must_change` tinyint(1) NOT NULL DEFAULT 0,
  `pass_updated_at` datetime DEFAULT NULL,
  `rol_id` int(11) NOT NULL,
  `estado` tinyint(1) NOT NULL DEFAULT 1,
  `fecha_creacion` timestamp NOT NULL DEFAULT current_timestamp(),
  `must_change_password` tinyint(1) NOT NULL DEFAULT 0,
  `password_changed_at` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `usuarios`
--

INSERT INTO `usuarios` (`id`, `cedula`, `nombres`, `apellidos`, `correo`, `contraseña`, `must_change`, `pass_updated_at`, `rol_id`, `estado`, `fecha_creacion`, `must_change_password`, `password_changed_at`) VALUES
(4, '10101010', 'Juan', 'Pérez', 'juan.perez@mail.com', '5ac0852e770506dcd80f1a36d20ba7878bf82244b836d9324593bd14bc56dcb5', 0, NULL, 1, 1, '2025-08-07 13:29:38', 0, NULL),
(5, '20202020', 'Ana', 'Gómez', 'ana.gomez@mail.com', 'ca8cc9fa7470c31c495c17f276152d9c9a4b06c20ecbfecffd65129f3de9b24d', 0, NULL, 2, 1, '2025-08-07 13:29:38', 0, NULL),
(6, '30303030', 'Luis', 'Martínez', 'luis.martinez@mail.com', 'aaf1d5e600c4dbe95b8f46f1d7e75f18ee2252a5df3d64141137d6bc6ca811aa', 0, NULL, 3, 1, '2025-08-07 13:29:38', 0, NULL),
(7, '40404040', 'Pedro', 'Ramírez', 'pedro.ramirez@mail.com', '03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4', 0, NULL, 1, 1, '2025-08-12 17:44:05', 0, NULL),
(8, '10203040', 'Marina', 'Caseres', 'mariac@gmail.com', '8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92', 0, NULL, 1, 1, '2025-08-13 13:33:31', 0, NULL),
(9, '1234512345', 'ALEX', 'FERNADEZ', 'alexf@gmail.com', '8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92', 0, NULL, 2, 1, '2025-08-13 15:07:43', 0, NULL),
(10, '1234567890', 'Fernanda', 'Garcia', 'garcia@gmail.com', '8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92', 0, NULL, 1, 1, '2025-08-13 15:15:47', 0, NULL),
(12, '123456789', 'Fernando', 'Gomez', 'fg@gmail.com', '8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92', 0, NULL, 1, 1, '2025-08-13 18:51:14', 0, NULL),
(14, '12345678', 'Marco', 'Hernandez', 'mhr@gmail.com', '8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92', 0, NULL, 1, 1, '2025-08-13 19:50:45', 0, NULL),
(16, '12343212', 'dana', 'martinez', 'da@gmail.com', '8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92', 0, NULL, 2, 1, '2025-08-13 20:28:20', 0, NULL);

--
-- Disparadores `usuarios`
--
DELIMITER $$
CREATE TRIGGER `trg_usuarios_bi_hash` BEFORE INSERT ON `usuarios` FOR EACH ROW BEGIN
  IF NEW.contraseña IS NOT NULL
     AND (CHAR_LENGTH(NEW.contraseña) <> 64 OR NEW.contraseña REGEXP '[^0-9A-Fa-f]') THEN
    SET NEW.contraseña = UPPER(SHA2(NEW.contraseña,256));
  END IF;
END
$$
DELIMITER ;
DELIMITER $$
CREATE TRIGGER `trg_usuarios_bu_hash` BEFORE UPDATE ON `usuarios` FOR EACH ROW BEGIN
  IF NEW.contraseña IS NOT NULL AND NEW.contraseña <> OLD.contraseña
     AND (CHAR_LENGTH(NEW.contraseña) <> 64 OR NEW.contraseña REGEXP '[^0-9A-Fa-f]') THEN
    SET NEW.contraseña = UPPER(SHA2(NEW.contraseña,256));
  END IF;
END
$$
DELIMITER ;

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `usuarios`
--
ALTER TABLE `usuarios`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `cedula` (`cedula`),
  ADD UNIQUE KEY `correo` (`correo`),
  ADD KEY `rol_id` (`rol_id`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `usuarios`
--
ALTER TABLE `usuarios`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=17;

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `usuarios`
--
ALTER TABLE `usuarios`
  ADD CONSTRAINT `usuarios_ibfk_1` FOREIGN KEY (`rol_id`) REFERENCES `roles` (`id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
