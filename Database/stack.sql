-- MySQL dump 10.13  Distrib 5.6.17, for Win64 (x86_64)
--
-- Host: localhost    Database: maislife
-- ------------------------------------------------------
-- Server version	5.6.23-log

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `bairro`
--

DROP TABLE IF EXISTS `bairro`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `bairro` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `nome` varchar(255) DEFAULT NULL,
  `taxa` decimal(5,2) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=205 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `bairro`
--

LOCK TABLES `bairro` WRITE;
/*!40000 ALTER TABLE `bairro` DISABLE KEYS */;
INSERT INTO `bairro` VALUES (2,'Adolfo Vireque',11.00),(3,'Aeroporto',12.00),(4,'Alto dos Passos',0.00),(5,'Alto dos Pinheiros',11.00),(6,'Alto Sumaré',23.00),(7,'Alphaville',27.00),(8,'Amazônia',11.00),(9,'Aracy',9.00),(10,'Araújo',19.00),(11,'Arco Íris',11.00),(12,'Bairu',7.00),(13,'Bandeirantes',9.00),(14,'Barbosa Lage',13.00),(15,'Barreira do Triunfo',23.00),(16,'Bela Aurora',11.00),(17,'Benfica',20.00),(18,'Boa Vista',8.00),(19,'Bom Clima',8.00),(20,'Bom Jardim',9.00),(21,'Bom Pastor',0.00),(22,'Bonfim',8.00),(23,'Bonsucesso',8.00),(24,'Borboleta',9.00),(25,'Borborema',8.00),(26,'Bosque do Imperador',14.50),(27,'Bosque dos Pinheiros',9.00),(28,'Bosque Imperial',9.00),(29,'Caiçaras',13.00),(30,'Caeté',41.00),(31,'Carlos Chagas',11.00),(32,'Carretão Gaúcho',27.00),(33,'Cascatiha',8.00),(34,'Centenário',7.00),(35,'Centro',0.00),(36,'Cerâmica',9.00),(37,'Cazario Alvim',7.00),(38,'Chacara',41.00),(39,'Chalés do Imperador',9.00),(40,'Chapéu D\'úvas',45.00),(41,'Cidade do Sol',14.50),(42,'Cidade Jardim',7.00),(43,'Cidade Nova',11.00),(44,'Cidade Unversitária',11.00),(45,'Condomínio Enseada',36.00),(46,'Condomínio Park Sul',18.50),(47,'Costa Carvalho',7.00),(48,'Cruzeiro do Sul',8.00),(49,'Democrata',8.00),(50,'Dias Tavares',32.00),(51,'Distrito Industrial',21.00),(52,'Dom Bosco',8.00),(53,'Dom Orione',9.00),(54,'Eldorado',8.00),(55,'Encosta do Sol',11.00),(56,'Esplanada',9.00),(57,'Estrela sul',8.00),(58,'Fábrica',9.00),(59,'Filgueiras',27.00),(60,'Floresta',23.00),(61,'Fontesville',13.50),(62,'Fontesville II',15.50),(63,'Francisco Bernadino',11.00),(64,'Furtado de Menezes',8.00),(65,'Grajaú',7.00),(66,'Grama',18.00),(67,'Graminha',16.50),(68,'Grambery',0.00),(69,'Granjas do Bosque',11.00),(70,'Granjas Santo Antônio',16.50),(71,'Granjas Betânia',12.00),(72,'Granjas Bethel',18.00),(73,'Granjas Guarujá',26.00),(74,'Granjas Primavera',12.00),(75,'Granjas Triunfo',23.00),(76,'Granville',9.00),(77,'Guaraú',8.00),(78,'Humaitá',36.00),(79,'Igrejinha',36.00),(80,'Industrial',11.00),(81,'Ipiranga',11.00),(82,'Jardim América',10.00),(83,'Jardim Casa Blanca',11.00),(84,'Jardim da serra',15.50),(85,'Jardim da Lua',9.00),(86,'Jardim de Alá',9.00),(87,'Jardim do Sol',8.00),(88,'Jardim dos Alfineiros',18.00),(89,'Jardim L\'Ermitage',20.00),(90,'Jardim Esperança',16.50),(91,'Jardim Gaúcho',13.00),(92,'Jardim Gloria',7.00),(93,'Jardim Laranjeiras',8.00),(94,'Jardim Liu',8.00),(95,'Jadim Natal',11.00),(96,'Jardins Imperiais',9.00),(97,'JK',9.00),(98,'Joquei Clube I',13.00),(99,'Joquei Clube II',14.50),(100,'Joquei Clube III',14.50),(101,'Ladeira',7.00),(102,'Linhares',12.00),(103,'Manoel Honório',7.00),(104,'Mansões do Bom Pastor',7.00),(105,'Mariano Procópio',7.00),(106,'Marilândia',14.50),(107,'Martelos',11.00),(108,'Marumbi',8.00),(109,'Matias Barbosa',36.00),(110,'Mercedes Benz',27.00),(111,'Milho Branco',11.00),(112,'Mirante BR 040',27.00),(113,'Monte Castelo',10.00),(114,'Monte Verde',54.00),(115,'Morada do Serro',11.00),(116,'Morro do Imperador',9.00),(117,'Mundo Novo',7.00),(118,'N. Sra. Aparecida',7.00),(119,'N. Sra. Das Graças',8.00),(120,'N. Sra. De Fátima',11.00),(121,'N. Sra. De Lourdes',8.00),(122,'Náutico',36.00),(123,'Nova Benfica',23.00),(124,'Nova California',14.50),(125,'Nova Era',18.00),(126,'Novo Horizonte',15.50),(127,'Pineiras',7.00),(128,'Palmital',54.00),(129,'Parque Burnier',8.00),(130,'Parque da Lajinha',11.00),(131,'Parque das Torres',14.50),(132,'Parque Guadalajara',10.00),(133,'Parque Guarani',11.00),(134,'Parque Imperial',9.00),(135,'Parque Independência',19.00),(136,'Parque das Águas',14.00),(137,'Parque Serra Verde',10.00),(138,'Pedra Bonita',16.60),(139,'Poço Rico',0.00),(140,'Ponte Preta',23.00),(141,'Portal da Torre',13.00),(142,'Previdenciários',13.00),(143,'Privilége',9.00),(144,'Progresso',8.00),(145,'Quintas da Avenida',8.00),(146,'Recanto dos Bugger',20.00),(147,'Recanto dos Lagos',16.50),(148,'Retiro',16.50),(149,'Rosário',54.00),(150,'Sagrado Coração',11.00),(151,'Salvaterra',16.50),(152,'Santa Câdida',8.00),(153,'Santa Catarina',7.00),(154,'Santa Cecília',7.00),(155,'Santa Cruz',22.00),(156,'Santa Efigênia',12.00),(157,'Santa Helena',0.00),(158,'Santa Isabel',18.00),(159,'Santa Lúcia',18.00),(160,'Santa Luzia',8.00),(161,'Santa Maria',13.50),(162,'Santa Paula',8.00),(163,'Santa Rita',9.00),(164,'Santa Tereza',7.00),(165,'Santa Terezinha',8.00),(166,'Santana',11.00),(167,'Santo Antônio',11.00),(168,'Santos Anjos',7.00),(169,'Santos Dumont',12.00),(170,'São Benedito',9.00),(171,'São Bernado',8.00),(172,'são Damião',21.00),(173,'São Dimas',8.00),(174,'São Francisco de Paula',18.00),(175,'São Geraldo',13.00),(176,'São Judas Tadeu',21.00),(177,'São Mateus',0.00),(178,'São Pedro',11.00),(179,'São Sebastião',8.00),(180,'São Tarcisio',8.00),(181,'Serro Azul',11.00),(182,'Solidariedade',10.00),(183,'Spina Ville',16.50),(184,'Teixeiras',9.00),(185,'Tiguera',10.00),(186,'Toledos',54.00),(187,'Torreões',45.00),(188,'Três Moinhos',9.00),(189,'Tupã',11.00),(190,'UFJF',10.00),(191,'Usina quatro',18.00),(192,'Vale do Amanhecer',11.00),(193,'Valadares',54.00),(194,'Vale do Ipê',7.00),(195,'Vale Verde',13.50),(196,'Vila Alpina',8.00),(197,'Vila Esperança',23.00),(198,'Vile Ideal',9.00),(199,'Vila Montanhesa',18.00),(200,'Vila Olavo costa',9.00),(201,'vila Ozanan',8.00),(202,'Vitorino Braga',7.00),(203,'Vivendas da Serra',11.00),(204,'Vivendas das Fontes',11.00);
/*!40000 ALTER TABLE `bairro` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `carrinho`
--

DROP TABLE IF EXISTS `carrinho`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `carrinho` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `usuario` int(11) DEFAULT NULL,
  `status` enum('Ativo','Fechado') DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_carrinho_usuario_idx` (`usuario`),
  CONSTRAINT `fk_carrinho_usuario` FOREIGN KEY (`usuario`) REFERENCES `usuario` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=76 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `carrinho`
--

LOCK TABLES `carrinho` WRITE;
/*!40000 ALTER TABLE `carrinho` DISABLE KEYS */;
INSERT INTO `carrinho` VALUES (30,11,'Fechado'),(31,11,'Fechado'),(32,11,'Fechado'),(33,11,'Fechado'),(36,11,'Fechado'),(40,11,'Fechado'),(41,11,'Fechado'),(46,11,'Fechado'),(47,11,'Fechado'),(48,11,'Fechado'),(49,11,'Fechado'),(50,11,'Fechado'),(51,11,'Fechado'),(52,11,'Fechado'),(53,11,'Fechado'),(54,11,'Fechado'),(55,11,'Fechado'),(56,11,'Fechado'),(57,11,'Fechado'),(58,11,'Fechado'),(59,11,'Fechado'),(60,11,'Fechado'),(61,11,'Fechado'),(63,11,'Fechado'),(69,11,'Fechado'),(70,11,'Fechado'),(71,11,'Fechado'),(72,11,'Fechado'),(73,12,'Ativo'),(74,11,'Fechado'),(75,11,'Ativo');
/*!40000 ALTER TABLE `carrinho` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `carrinho_produto`
--

DROP TABLE IF EXISTS `carrinho_produto`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `carrinho_produto` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `produto` int(11) DEFAULT NULL,
  `carrinho` int(11) DEFAULT NULL,
  `quantidade` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_carrinhoProduto_produto_idx` (`produto`),
  KEY `fk_carrinhoProduto_carrinho_idx` (`carrinho`),
  CONSTRAINT `fk_carrinhoProduto_carrinho` FOREIGN KEY (`carrinho`) REFERENCES `carrinho` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_carrinhoProduto_produto` FOREIGN KEY (`produto`) REFERENCES `produto` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=140 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `carrinho_produto`
--

LOCK TABLES `carrinho_produto` WRITE;
/*!40000 ALTER TABLE `carrinho_produto` DISABLE KEYS */;
INSERT INTO `carrinho_produto` VALUES (104,10,46,5),(105,10,46,4),(106,10,46,5),(107,10,46,5),(108,10,46,6),(109,10,47,2),(110,10,48,2),(111,10,49,3),(112,10,50,4),(113,11,51,10),(114,11,52,4),(115,10,53,3),(116,10,54,2),(117,11,54,3),(118,11,55,3),(119,10,55,5),(120,10,56,3),(121,10,57,2),(122,10,58,5),(123,10,59,10),(124,11,60,10),(127,11,63,5),(128,11,61,20),(129,10,30,1),(131,11,69,5),(132,11,70,5),(133,10,70,3),(134,11,71,5),(135,10,71,2),(136,11,72,2),(137,13,72,1),(138,11,74,3),(139,10,72,1);
/*!40000 ALTER TABLE `carrinho_produto` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `contato`
--

DROP TABLE IF EXISTS `contato`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `contato` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `nome` varchar(45) DEFAULT NULL,
  `assunto` varchar(45) DEFAULT NULL,
  `email` varchar(45) DEFAULT NULL,
  `telefone` varchar(45) DEFAULT NULL,
  `mensagem` varchar(500) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `contato`
--

LOCK TABLES `contato` WRITE;
/*!40000 ALTER TABLE `contato` DISABLE KEYS */;
/*!40000 ALTER TABLE `contato` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `devolucao`
--

DROP TABLE IF EXISTS `devolucao`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `devolucao` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `data` datetime NOT NULL,
  `tipo` enum('Venda externa','Site') NOT NULL DEFAULT 'Venda externa',
  `cliente` int(11) NOT NULL,
  `motivo` text,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `devolucao`
--

LOCK TABLES `devolucao` WRITE;
/*!40000 ALTER TABLE `devolucao` DISABLE KEYS */;
/*!40000 ALTER TABLE `devolucao` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `devolucao_produto`
--

DROP TABLE IF EXISTS `devolucao_produto`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `devolucao_produto` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `devolucao` int(11) NOT NULL,
  `produto` int(11) NOT NULL,
  `quantidade` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_devolucao_produto_devolucao_idx` (`devolucao`),
  KEY `fk_devolucao_produto_produto_idx` (`produto`),
  CONSTRAINT `fk_devolucao_produto_devolucao` FOREIGN KEY (`devolucao`) REFERENCES `devolucao` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_devolucao_produto_produto` FOREIGN KEY (`produto`) REFERENCES `produto` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `devolucao_produto`
--

LOCK TABLES `devolucao_produto` WRITE;
/*!40000 ALTER TABLE `devolucao_produto` DISABLE KEYS */;
/*!40000 ALTER TABLE `devolucao_produto` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `endereco`
--

DROP TABLE IF EXISTS `endereco`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `endereco` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `usuario` int(11) DEFAULT NULL,
  `pais` varchar(255) DEFAULT NULL,
  `estado` varchar(2) DEFAULT NULL,
  `cidade` varchar(255) DEFAULT NULL,
  `bairro` int(11) DEFAULT NULL,
  `rua` varchar(255) DEFAULT NULL,
  `numero` varchar(255) DEFAULT NULL,
  `cep` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_endereco_usuario_idx` (`usuario`),
  KEY `fk_endereco_bairro_idx` (`bairro`),
  CONSTRAINT `fk_endereco_bairro` FOREIGN KEY (`bairro`) REFERENCES `bairro` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_endereco_usuario` FOREIGN KEY (`usuario`) REFERENCES `usuario` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `endereco`
--

LOCK TABLES `endereco` WRITE;
/*!40000 ALTER TABLE `endereco` DISABLE KEYS */;
INSERT INTO `endereco` VALUES (1,11,'Brasil','MG','Juiz de Fora',81,'Av Darcy Vargas 713','713',NULL),(2,NULL,'Brasil','MG','Juiz de Fora',81,'Av Darcy Vargas 713','713',NULL),(3,NULL,'Brasil','MG','Juiz de Fora',3,'Av João do Aeroporto','111',NULL),(6,NULL,'Brasil','MG','sdfsdfs',81,'fsdfsdfsd','100',NULL),(7,11,'Brasil','MG','Juiz de Fora',3,'Av São João','814',NULL);
/*!40000 ALTER TABLE `endereco` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `entrada`
--

DROP TABLE IF EXISTS `entrada`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `entrada` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `numero` int(11) NOT NULL,
  `data` datetime NOT NULL,
  `fornecedor` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_entrada_fornecedor_idx` (`fornecedor`),
  CONSTRAINT `fk_entrada_fornecedor` FOREIGN KEY (`fornecedor`) REFERENCES `fornecedor` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `entrada`
--

LOCK TABLES `entrada` WRITE;
/*!40000 ALTER TABLE `entrada` DISABLE KEYS */;
/*!40000 ALTER TABLE `entrada` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `entrada_estoque`
--

DROP TABLE IF EXISTS `entrada_estoque`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `entrada_estoque` (
  `codigo` int(11) NOT NULL,
  `entrada` int(11) NOT NULL,
  `estoque` int(11) NOT NULL,
  `data_entrada` datetime DEFAULT NULL,
  PRIMARY KEY (`codigo`),
  KEY `fk_entrada_estoque_entrada_idx` (`entrada`),
  KEY `fk_entrada_estoque_estoque_idx` (`estoque`),
  CONSTRAINT `fk_entrada_estoque_entrada` FOREIGN KEY (`entrada`) REFERENCES `entrada` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_entrada_estoque_estoque` FOREIGN KEY (`estoque`) REFERENCES `estoque` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `entrada_estoque`
--

LOCK TABLES `entrada_estoque` WRITE;
/*!40000 ALTER TABLE `entrada_estoque` DISABLE KEYS */;
/*!40000 ALTER TABLE `entrada_estoque` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `estoque`
--

DROP TABLE IF EXISTS `estoque`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `estoque` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `tipo` enum('coco','saco') NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `estoque`
--

LOCK TABLES `estoque` WRITE;
/*!40000 ALTER TABLE `estoque` DISABLE KEYS */;
/*!40000 ALTER TABLE `estoque` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `fornecedor`
--

DROP TABLE IF EXISTS `fornecedor`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `fornecedor` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `razao_social` varchar(45) DEFAULT NULL,
  `documento` varchar(45) DEFAULT NULL,
  `nome` varchar(45) NOT NULL,
  `telefone` varchar(45) NOT NULL,
  `email` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `fornecedor`
--

LOCK TABLES `fornecedor` WRITE;
/*!40000 ALTER TABLE `fornecedor` DISABLE KEYS */;
/*!40000 ALTER TABLE `fornecedor` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `mapa_pedido`
--

DROP TABLE IF EXISTS `mapa_pedido`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `mapa_pedido` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `mapa` int(11) NOT NULL,
  `pedido` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_mapa_pedido_mapa_idx` (`mapa`),
  KEY `fk_mapa_pedido_pedido_idx` (`pedido`),
  CONSTRAINT `fk_mapa_pedido_mapa` FOREIGN KEY (`mapa`) REFERENCES `mapaentrega` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_mapa_pedido_pedido` FOREIGN KEY (`pedido`) REFERENCES `pedido` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=138 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `mapa_pedido`
--

LOCK TABLES `mapa_pedido` WRITE;
/*!40000 ALTER TABLE `mapa_pedido` DISABLE KEYS */;
INSERT INTO `mapa_pedido` VALUES (131,27,49),(132,27,47),(133,27,51),(134,27,50),(135,27,52),(136,27,53);
/*!40000 ALTER TABLE `mapa_pedido` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `mapaentrega`
--

DROP TABLE IF EXISTS `mapaentrega`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `mapaentrega` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `observacao` text,
  `data_entrega` date DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=29 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `mapaentrega`
--

LOCK TABLES `mapaentrega` WRITE;
/*!40000 ALTER TABLE `mapaentrega` DISABLE KEYS */;
INSERT INTO `mapaentrega` VALUES (27,'drdrd','2016-09-21');
/*!40000 ALTER TABLE `mapaentrega` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `parceiro`
--

DROP TABLE IF EXISTS `parceiro`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `parceiro` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `nome` varchar(255) NOT NULL,
  `imagem` varchar(255) NOT NULL,
  `enderec` varchar(255) NOT NULL,
  `telefone` varchar(255) DEFAULT NULL,
  `site` varchar(255) DEFAULT NULL,
  `facebook` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `parceiro`
--

LOCK TABLES `parceiro` WRITE;
/*!40000 ALTER TABLE `parceiro` DISABLE KEYS */;
INSERT INTO `parceiro` VALUES (2,'tretger','13434875_10208005759400025_6934240626818496805_n (1).jpg','terte','rterter','tertreter',NULL);
/*!40000 ALTER TABLE `parceiro` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `pedido`
--

DROP TABLE IF EXISTS `pedido`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `pedido` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `tipo` enum('Venda','Troca','Bonificado','Merchandising') DEFAULT 'Venda',
  `origem` enum('Vendedor','Site') DEFAULT 'Site',
  `data` datetime NOT NULL,
  `metodo` enum('A vista','Consignado','Boleto','Prazo') DEFAULT 'A vista',
  `usuario` int(11) NOT NULL,
  `carrinho` int(11) NOT NULL,
  `usuario_externo` int(11) DEFAULT NULL,
  `endereco` int(11) NOT NULL,
  `valor` decimal(10,2) NOT NULL,
  `status` enum('Em aberto','Em trânsito','Entregue') DEFAULT 'Em aberto',
  `pago` decimal(10,2) DEFAULT NULL,
  `desconto` int(11) DEFAULT '0',
  `parcelas` int(11) DEFAULT '0',
  `vencimento` date DEFAULT NULL,
  `motivo_troca` text,
  `previsao_entrega` date DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_pedido_usuario_idx` (`usuario`),
  KEY `fk_pedido_endereco_idx` (`endereco`),
  KEY `fk_pedido_carrinho_idx` (`carrinho`),
  KEY `fk_pedido_usuario_externo_idx` (`usuario_externo`),
  CONSTRAINT `fk_pedido_carrinho` FOREIGN KEY (`carrinho`) REFERENCES `carrinho` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_pedido_endereco` FOREIGN KEY (`endereco`) REFERENCES `endereco` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_pedido_usuario` FOREIGN KEY (`usuario`) REFERENCES `usuario` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_pedido_usuario_externo` FOREIGN KEY (`usuario_externo`) REFERENCES `usuario_externo` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=56 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `pedido`
--

LOCK TABLES `pedido` WRITE;
/*!40000 ALTER TABLE `pedido` DISABLE KEYS */;
INSERT INTO `pedido` VALUES (32,'Troca','Vendedor','2016-07-11 23:35:25','Boleto',11,46,1,2,24750.00,'Entregue',0.00,21,NULL,'2016-07-28','TROCA DOIDA','2016-07-11'),(33,'Venda','Vendedor','2016-07-12 13:21:39','Prazo',11,47,1,2,1980.00,'Entregue',0.00,10,15,NULL,NULL,'2016-07-16'),(34,'Venda','Vendedor','2016-07-12 17:21:48','Prazo',11,48,1,2,1980.00,'Entregue',0.00,10,20,NULL,NULL,'2016-07-15'),(35,'Venda','Vendedor','2016-07-12 18:38:32','Boleto',11,49,2,3,2970.00,'Entregue',0.00,4,0,'2016-07-29',NULL,'2016-07-15'),(36,'Bonificado','Vendedor','2016-07-12 18:42:13','Prazo',11,50,1,2,3960.00,'Entregue',0.00,20,4,NULL,NULL,'2016-07-18'),(39,'Venda','Vendedor','2016-07-13 21:09:00','Prazo',11,53,2,3,2970.00,'Entregue',0.00,20,10,NULL,NULL,'2016-07-19'),(40,'Troca','Vendedor','2016-07-13 21:09:30','A vista',11,54,5,6,2040.00,'Entregue',0.00,10,0,NULL,'dds fsd fs','2016-07-19'),(41,'Venda','Vendedor','2016-07-13 21:09:46','Prazo',11,55,1,2,5010.00,'Entregue',0.00,0,20,NULL,NULL,'2016-07-19'),(42,'Venda','Vendedor','2016-07-13 21:09:59','Prazo',11,56,2,3,2970.00,'Entregue',0.00,20,5,NULL,NULL,'2016-07-19'),(43,'Venda','Vendedor','2016-07-13 21:16:04','A vista',11,57,1,2,1980.00,'Entregue',0.00,10,0,NULL,NULL,'2016-07-19'),(44,'Venda','Vendedor','2016-07-14 11:23:02','A vista',11,58,1,2,4950.00,'Entregue',0.00,10,0,NULL,NULL,'2016-07-20'),(45,'Venda','Vendedor','2016-07-14 11:24:46','A vista',11,59,1,2,9900.00,'Entregue',0.00,10,0,NULL,NULL,'2016-07-20'),(46,'Venda','Vendedor','2016-07-14 11:27:54','A vista',11,60,1,2,180.00,'Entregue',0.00,10,0,NULL,NULL,'2016-07-17'),(47,'Troca','Vendedor','2016-07-14 13:15:29','A vista',11,61,1,2,400.00,'Em trânsito',0.00,0,NULL,NULL,'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aliquam odio neque, facilisis quis dapibus id, elementum et velit. Aliquam erat volutpat. Aenean est nisl, auctor id sollicitudin feugiat, mattis eget nisl. Proin pellentesque ornare venenatis. Maecenas eget tincidunt sapien. Nunc mollis porttitor sapien eu pharetra. Proin molestie nunc sed est pulvinar, at accumsan urna accumsan. Sed vehicula pharetra dolor, vitae faucibus neque. Nam vel suscipit lorem.\r\n\r\nPellentesque id magna eget dolor faucibus suscipit. Cras accumsan orci ut mi aliquam fringilla. Suspendisse consectetur ut nisi in malesuada. Suspendisse vehicula, felis non congue efficitur, nisi nisi ornare purus, sed semper leo tortor at nisi. Nam quam sem, tristique vel dolor sit amet, commodo semper erat. Cras dapibus pulvinar pharetra. Cras mattis nunc sed erat porttitor dignissim. Cras sollicitudin dolor vitae purus dictum gravida. Maecenas nisi lectus, commodo id massa interdum, efficitur sagittis nunc. Donec non feugiat lacus. Phasellus et faucibus justo. Pellentesque ut sagittis lorem.','2016-07-21'),(49,'Venda','Vendedor','2016-07-17 12:40:17','A vista',11,63,5,6,100.00,'Em trânsito',0.00,0,0,NULL,NULL,'2016-07-20'),(50,'Venda','Site','2016-07-18 14:36:41','A vista',11,30,NULL,7,1002.00,'Em trânsito',1010.00,0,0,NULL,NULL,'2016-07-24'),(51,'Venda','Site','2016-07-18 15:04:54','A vista',11,69,NULL,7,112.00,'Em trânsito',1000.00,0,0,NULL,NULL,'2016-07-21'),(52,'Venda','Site','2016-07-18 15:16:53','Prazo',11,70,NULL,7,3082.00,'Em trânsito',0.00,0,3,NULL,NULL,'2016-07-24'),(53,'Venda','Site','2016-07-18 15:18:06','Prazo',11,71,NULL,7,2092.00,'Em trânsito',0.00,0,2,NULL,NULL,'2016-07-24'),(54,'Venda','Vendedor','2016-10-10 18:27:20','A vista',11,74,1,2,60.00,'Entregue',0.00,0,0,NULL,NULL,'2016-10-13'),(55,'Venda','Site','2016-10-10 18:50:01','Prazo',11,72,NULL,7,1042.00,'Em aberto',0.00,0,1,NULL,NULL,'2016-10-16');
/*!40000 ALTER TABLE `pedido` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `produto`
--

DROP TABLE IF EXISTS `produto`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `produto` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `nome` varchar(255) NOT NULL,
  `descricao` text NOT NULL,
  `preco` decimal(10,2) NOT NULL,
  `imagem` varchar(255) DEFAULT NULL,
  `unidade` int(11) DEFAULT '1',
  `dias_entrega` int(11) NOT NULL DEFAULT '1',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `produto`
--

LOCK TABLES `produto` WRITE;
/*!40000 ALTER TABLE `produto` DISABLE KEYS */;
INSERT INTO `produto` VALUES (10,'fsdfsdf99','hfd gfd hgfh gfh gfh gfh gfh fg99',990.00,NULL,99,6),(11,'Teste2','hgfh fg hfg',20.00,'icon_sell.png',5,3),(12,'Água de coco ','Lorme ipsum',0.00,'kick.jpg',5,3),(13,'y fg ffg hfg','Lorem  Ipsum',0.00,NULL,5,0);
/*!40000 ALTER TABLE `produto` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `produto_bairro`
--

DROP TABLE IF EXISTS `produto_bairro`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `produto_bairro` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `produto` int(11) NOT NULL,
  `bairro` int(11) NOT NULL,
  `taxa` decimal(10,2) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_produtoBairro_produto_idx` (`produto`),
  KEY `fk_produtoBairro_bairro_idx` (`bairro`),
  CONSTRAINT `fk_produtoBairro_bairro` FOREIGN KEY (`bairro`) REFERENCES `bairro` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_produtoBairro_produto` FOREIGN KEY (`produto`) REFERENCES `produto` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=57 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `produto_bairro`
--

LOCK TABLES `produto_bairro` WRITE;
/*!40000 ALTER TABLE `produto_bairro` DISABLE KEYS */;
INSERT INTO `produto_bairro` VALUES (50,10,18,101.55),(51,10,17,99.05),(52,11,17,20.00),(53,12,17,2000.00),(54,12,19,2044.00),(56,13,5,22.00);
/*!40000 ALTER TABLE `produto_bairro` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `usuario`
--

DROP TABLE IF EXISTS `usuario`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `usuario` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `nome` varchar(255) DEFAULT NULL,
  `sobrenome` varchar(255) DEFAULT NULL,
  `email` varchar(255) DEFAULT NULL,
  `senha` varchar(255) DEFAULT NULL,
  `tipo` enum('Administrador','Vendedor','Cliente') DEFAULT NULL,
  `permissao` int(11) DEFAULT '0',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `usuario`
--

LOCK TABLES `usuario` WRITE;
/*!40000 ALTER TABLE `usuario` DISABLE KEYS */;
INSERT INTO `usuario` VALUES (11,'Maycon','Teixeira','tmaycon1@gmail.com','abc123','Administrador',2),(12,'Tiago','Sales','tihhjf@hotmail.com','abc123','Vendedor',1);
/*!40000 ALTER TABLE `usuario` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `usuario_externo`
--

DROP TABLE IF EXISTS `usuario_externo`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `usuario_externo` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `nome` varchar(255) NOT NULL,
  `documento` varchar(45) NOT NULL,
  `telefone` varchar(55) NOT NULL,
  `endereco` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_usuarioExterno_endereco_idx` (`endereco`),
  CONSTRAINT `fk_usuarioExterno_endereco` FOREIGN KEY (`endereco`) REFERENCES `endereco` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `usuario_externo`
--

LOCK TABLES `usuario_externo` WRITE;
/*!40000 ALTER TABLE `usuario_externo` DISABLE KEYS */;
INSERT INTO `usuario_externo` VALUES (1,'Bahamas','167.658.623.325478/2','32345268',2),(2,'João','11709501677','32345268',3),(5,'fdsf','fdsfsdf','dsfds',6);
/*!40000 ALTER TABLE `usuario_externo` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2016-10-10 19:05:57
