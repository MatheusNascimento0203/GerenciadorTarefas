function formatarData(dataString) {
  var partesData = dataString.split(" ");
  var data = partesData[0];

  var partes = data.split("/");
  var dia = partes[0];
  var mes = parseInt(partes[1], 10);
  var ano = partes[2];

  // Array com os nomes dos meses
  var meses = [
    "Janeiro",
    "Fevereiro",
    "Março",
    "Abril",
    "Maio",
    "Junho",
    "Julho",
    "Agosto",
    "Setembro",
    "Outubro",
    "Novembro",
    "Dezembro",
  ];

  // Montando a data formatada
  var dataFormatada = dia + " de " + meses[mes - 1] + " de " + ano;

  return dataFormatada;
}
