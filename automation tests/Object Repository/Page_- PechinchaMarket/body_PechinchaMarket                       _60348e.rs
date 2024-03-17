<?xml version="1.0" encoding="UTF-8"?>
<WebElementEntity>
   <description></description>
   <name>body_PechinchaMarket                       _60348e</name>
   <tag></tag>
   <elementGuidId>0b40cfe1-2acf-47d6-be08-d6f5596a3ac4</elementGuidId>
   <selectorCollection>
      <entry>
         <key>CSS</key>
         <value>body</value>
      </entry>
      <entry>
         <key>XPATH</key>
         <value>//body</value>
      </entry>
   </selectorCollection>
   <selectorMethod>XPATH</selectorMethod>
   <smartLocatorCollection>
      <entry>
         <key>SMART_LOCATOR</key>
         <value>internal:text=&quot;PechinchaMarket Register Login Ordenar ↓ Preço Descendente Preço Ascendente Marc&quot;i</value>
      </entry>
   </smartLocatorCollection>
   <smartLocatorEnabled>false</smartLocatorEnabled>
   <useRalativeImagePath>true</useRalativeImagePath>
   <webElementProperties>
      <isSelected>false</isSelected>
      <matchCondition>equals</matchCondition>
      <name>tag</name>
      <type>Main</type>
      <value>body</value>
      <webElementGuid>ca020c85-8e2d-4d61-89af-8e521e048111</webElementGuid>
   </webElementProperties>
   <webElementProperties>
      <isSelected>true</isSelected>
      <matchCondition>equals</matchCondition>
      <name>text</name>
      <type>Main</type>
      <value>
    
        
            
                

                     PechinchaMarket

                
                
                    
                
                
                    

    
        
            
                
                
            
            
        
    
    
    
            
                Register
            
            
                Login
            
        
    


    document.addEventListener('DOMContentLoaded', function () {
        fetchPerfilImage();
    });

    function fetchPerfilImage() {
        fetch('/Search/GetPerfilImage', {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
                // Add any additional headers if needed
            }
        })
            .then(response => {
                if (!response.ok) {
                    throw new Error('Network response was not ok');
                }
                return response.json(); // Assuming the response is in JSON format
            })
            .then(data => {
                const imageSrc = `url('data:image/jpeg;base64,${data}')`;

                document.getElementById('manage').style.backgroundImage = imageSrc;
            })
            .catch(error => {
                console.error('There was a problem with the fetch operation:', error);
            });
    }

    function displayperfilmenu() { 
        var menu = document.getElementById('perfil-menu');
        menu.classList.toggle('display-none');
    }

                
            
        
    
    
        
            






    
            
                
                   
                    
                        Ordenar ↓ 
                        
                            Preço Descendente
                            Preço Ascendente
                            Marca Ascendente
                            Marca Descendente
                        
                    
                     

                   
                
                
                    
                        
                            Filtrar Por:
                            
                                Zona:
                                
                                    em progresso
                                    em progresso
                                
                            
                            
                                Comerciante:
                                
                                        
                                            
                                            ComerciantePMK
                                        
                                    
                                        
                                
                            
                            
                                Categoria:
                                
                                        
                                            
                                            Enlatados
                                        
                                        
                                            
                                            Frescos
                                        
                                        
                                            
                                            Biologicos
                                        
                                        
                                            
                                            Congelados
                                        
                                        
                                            
                                            Pastelaria
                                        
                                        
                                            
                                            Talho
                                        
                                        
                                            
                                            Peixaria
                                        
                                        
                                            
                                            Charcutaria
                                        
                                        
                                            
                                            Bebidas
                                        
                                        
                                            
                                            Vegan
                                        
                                        
                                            
                                            Doces
                                        
                                        
                                            
                                            Snacks
                                        
                                        
                                            
                                            BebidasAlcoólicas
                                        


                                
                            
                            
                                Preço:
                                
                                    
                                        
                                            
                                            
                                        
                                        
                                            
                                                Min
                                                
                                            
                                            
                                                Max
                                                
                                            
                                        
                                    
                                
                            
                        
                    
                    
                                
                                    
                                    Atum - ComerciantePMK
                                    Enlatados
                                    Bom Petisco
                                    1.2 €/Unit
                                
                                
                                    
                                    Manteiga - ComerciantePMK
                                    Frescos
                                    Mimosa
                                    2.25 €/Unit
                                
                                
                                    
                                    Manteiga Vegetal - ComerciantePMK
                                    Frescos
                                    Planta
                                    3.3 €/Unit
                                
                                
                                    
                                    Manteiga - ComerciantePMK
                                    Frescos
                                    Gresso
                                    40 €/Unit
                                
                    
                  
                
            
    



    function filtrar() {

        var produtos = document.getElementById(&quot;search-results&quot;).children;

        var categoria = document.getElementsByClassName(&quot;categorias&quot;);
        var categoriasSelected = Array.from(categoria);
        var comerciantes = document.getElementsByClassName(&quot;comerciantes&quot;);
        var comerciantesSelected = Array.from(comerciantes);

        var precoMin = document.getElementById(&quot;fromInput&quot;).value;
        var precoMax = document.getElementById(&quot;toInput&quot;).value;

        for (item of Array.from(comerciantes)) {
            if (item.checked == false) {
                var i = comerciantesSelected.indexOf(item);
                comerciantesSelected.splice(i, 1);
            }
        }

        for (item of Array.from(categorias)) {
            if (item.checked == false) {
                var i = categoriasSelected.indexOf(item);
                categoriasSelected.splice(i, 1);
            }
        }

        for(item of produtos){
            if (parseFloat(item.querySelector(&quot;#price&quot;).textContent) > precoMax) {
                item.style.display = 'none';
            } 
            else if (parseFloat(item.querySelector(&quot;#price&quot;).textContent) &lt; precoMin) {
                item.style.display = 'none';
            } else {
                item.style.display = &quot;flex&quot;;
            }

            if(categoriasSelected.length > 0){
                var b = false;
                for(cat of categoriasSelected){
                    if (item.textContent.includes(cat.value)){
                        b = true;
                    }
                }
                if(!b){
                    item.style.display = 'none';
                }
            }
            
            if (comerciantesSelected.length > 0) {
                var b = false;
                for (com of comerciantesSelected) {
                    if (item.textContent.includes(com.value)) {
                        b = true;
                    }
                }
                if (!b) {
                    item.style.display = 'none';
                }
            }
            
        }

    }



    //slider
    function controlFromInput(fromSlider, fromInput, toInput, controlSlider) {
        const [from, to] = getParsed(fromInput, toInput);
        fillSlider(fromInput, toInput, '#C6C6C6', '#25daa5', controlSlider);
        if (from > to) {
            fromSlider.value = to;
            fromInput.value = to;
        } else {
            fromSlider.value = from;
        }
    }

    function controlToInput(toSlider, fromInput, toInput, controlSlider) {
        const [from, to] = getParsed(fromInput, toInput);
        fillSlider(fromInput, toInput, '#C6C6C6', '#25daa5', controlSlider);
        setToggleAccessible(toInput);
        if (from &lt;= to) {
            toSlider.value = to;
            toInput.value = to;
        } else {
            toInput.value = from;
        }
    }

    function controlFromSlider(fromSlider, toSlider, fromInput) {
        const [from, to] = getParsed(fromSlider, toSlider);
        fillSlider(fromSlider, toSlider, '#C6C6C6', '#25daa5', toSlider);
        if (from > to) {
            fromSlider.value = to;
            fromInput.value = to;
        } else {
            fromInput.value = from;
        }
    }

    function controlToSlider(fromSlider, toSlider, toInput) {
        const [from, to] = getParsed(fromSlider, toSlider);
        fillSlider(fromSlider, toSlider, '#C6C6C6', '#25daa5', toSlider);
        setToggleAccessible(toSlider);
        if (from &lt;= to) {
            toSlider.value = to;
            toInput.value = to;
        } else {
            toInput.value = from;
            toSlider.value = from;
        }
    }

    function getParsed(currentFrom, currentTo) {
        const from = parseFloat(currentFrom.value, 10);
        const to = parseFloat(currentTo.value, 10);
        return [from, to];
    }

    function fillSlider(from, to, sliderColor, rangeColor, controlSlider) {
        const rangeDistance = to.max - to.min;
        const fromPosition = from.value - to.min;
        const toPosition = to.value - to.min;
        controlSlider.style.background = `linear-gradient(
          to right,
          ${sliderColor} 0%,
          ${sliderColor} ${(fromPosition) / (rangeDistance) * 100}%,
          ${rangeColor} ${((fromPosition) / (rangeDistance)) * 100}%,
          ${rangeColor} ${(toPosition) / (rangeDistance) * 100}%,
          ${sliderColor} ${(toPosition) / (rangeDistance) * 100}%,
          ${sliderColor} 100%)`;
    }

    function setToggleAccessible(currentTarget) {
        const toSlider = document.querySelector('#toSlider');
        if (Number(currentTarget.value) &lt;= 0) {
            toSlider.style.zIndex = 2;
        } else {
            toSlider.style.zIndex = 0;
        }
    }

    const fromSlider = document.querySelector('#fromSlider');
    const toSlider = document.querySelector('#toSlider');
    const fromInput = document.querySelector('#fromInput');
    const toInput = document.querySelector('#toInput');
    fillSlider(fromSlider, toSlider, '#C6C6C6', '#25daa5', toSlider);
    setToggleAccessible(toSlider);

    fromSlider.oninput = () => controlFromSlider(fromSlider, toSlider, fromInput);
    toSlider.oninput = () => controlToSlider(fromSlider, toSlider, toInput);
    fromInput.oninput = () => controlFromInput(fromSlider, fromInput, toInput, toSlider);
    toInput.oninput = () => controlToInput(toSlider, fromInput, toInput, toSlider);

    //collapsibles

    var coll = document.getElementsByClassName(&quot;collapsible&quot;);
    var i;

    for (i = 0; i &lt; coll.length; i++) {
        coll[i].addEventListener(&quot;click&quot;, function () {
            this.classList.toggle(&quot;active&quot;);
            var content = this.nextElementSibling;
            if (content.style.maxHeight) {
                content.style.maxHeight = null;
            } else {
                content.style.maxHeight = content.scrollHeight + &quot;px&quot;;
            }
        });
    }

    function sortResultsByPriceDesc(){
        var resultsContainer = document.getElementById(&quot;search-results&quot;);

        var results = Array.from(document.getElementById(&quot;search-results&quot;).children);

    
        function sortByPrice(a, b) {
            var priceA = parseFloat(a.getElementById(&quot;price&quot;).textContent.replace(/[^\d.]/g, ''));
            var priceB = parseFloat(b.getElementById(&quot;price&quot;).textContent.replace(/[^\d.]/g, ''));
            return priceB - priceA;
        }
        results.sort(sortByPrice);
        resultsContainer.innerHTML = '';

        results.forEach(function (result) {
            resultsContainer.appendChild(result);
        }); 
       
    }
    function sortResultsByPriceAsc() {
        var resultsContainer = document.getElementById(&quot;search-results&quot;);

        var results = Array.from(document.getElementById(&quot;search-results&quot;).children);


        function sortByPrice(a, b) {
            var priceA = parseFloat(a.getElementById(&quot;price&quot;).textContent.replace(/[^\d.]/g, ''));
            var priceB = parseFloat(b.getElementById(&quot;price&quot;).textContent.replace(/[^\d.]/g, ''));
            return priceA - priceB;
        }
        results.sort(sortByPrice);
        resultsContainer.innerHTML = '';

        results.forEach(function (result) {
            resultsContainer.appendChild(result);
        });
    }

    function sortResultsByBrandAsc() {
        var resultsContainer = document.getElementById(&quot;search-results&quot;);
        var results = Array.from(document.getElementById(&quot;search-results&quot;).children);

  

        function sortByBrand(a, b) {
            var brandA = a.getElementById(&quot;brand&quot;).textContent;
            var brandB = b.getElementById(&quot;brand&quot;).textContent;

            return brandA.localeCompare(brandB);
        }

        results.sort(sortByBrand);
        resultsContainer.innerHTML = '';

        results.forEach(function (result) {
            resultsContainer.appendChild(result);
        });
    }
    function sortResultsByBrandDesc() {
        var resultsContainer = document.getElementById(&quot;search-results&quot;);
        var results = Array.from(document.getElementById(&quot;search-results&quot;).children);

      

        function sortByBrand(a, b) {
            var brandA = a.getElementById(&quot;brand&quot;).textContent;
            var brandB = b.getElementById(&quot;brand&quot;).textContent;

            return brandB.localeCompare(brandA);
        }

        results.sort(sortByBrand);
        resultsContainer.innerHTML = '';

        results.forEach(function (result) {
            resultsContainer.appendChild(result);
        });
    }


        
    

    
    
    
    


/html[1]/body[1]</value>
      <webElementGuid>70e5a83b-7a31-4ceb-9036-25df371d815d</webElementGuid>
   </webElementProperties>
   <webElementProperties>
      <isSelected>false</isSelected>
      <matchCondition>equals</matchCondition>
      <name>xpath</name>
      <type>Main</type>
      <value>/html[1]/body[1]</value>
      <webElementGuid>bf0753de-d430-4faf-92b5-3d014a24287d</webElementGuid>
   </webElementProperties>
   <webElementXpaths>
      <isSelected>true</isSelected>
      <matchCondition>equals</matchCondition>
      <name>xpath:position</name>
      <type>Main</type>
      <value>//body</value>
      <webElementGuid>b6e20d7f-f7b3-49a7-9897-b75e55f54446</webElementGuid>
   </webElementXpaths>
   <webElementXpaths>
      <isSelected>false</isSelected>
      <matchCondition>equals</matchCondition>
      <name>xpath:customAttributes</name>
      <type>Main</type>
      <value>//body[(text() = concat(&quot;
    
        
            
                

                     PechinchaMarket

                
                
                    
                
                
                    

    
        
            
                
                
            
            
        
    
    
    
            
                Register
            
            
                Login
            
        
    


    document.addEventListener(&quot; , &quot;'&quot; , &quot;DOMContentLoaded&quot; , &quot;'&quot; , &quot;, function () {
        fetchPerfilImage();
    });

    function fetchPerfilImage() {
        fetch(&quot; , &quot;'&quot; , &quot;/Search/GetPerfilImage&quot; , &quot;'&quot; , &quot;, {
            method: &quot; , &quot;'&quot; , &quot;GET&quot; , &quot;'&quot; , &quot;,
            headers: {
                &quot; , &quot;'&quot; , &quot;Content-Type&quot; , &quot;'&quot; , &quot;: &quot; , &quot;'&quot; , &quot;application/json&quot; , &quot;'&quot; , &quot;,
                // Add any additional headers if needed
            }
        })
            .then(response => {
                if (!response.ok) {
                    throw new Error(&quot; , &quot;'&quot; , &quot;Network response was not ok&quot; , &quot;'&quot; , &quot;);
                }
                return response.json(); // Assuming the response is in JSON format
            })
            .then(data => {
                const imageSrc = `url(&quot; , &quot;'&quot; , &quot;data:image/jpeg;base64,${data}&quot; , &quot;'&quot; , &quot;)`;

                document.getElementById(&quot; , &quot;'&quot; , &quot;manage&quot; , &quot;'&quot; , &quot;).style.backgroundImage = imageSrc;
            })
            .catch(error => {
                console.error(&quot; , &quot;'&quot; , &quot;There was a problem with the fetch operation:&quot; , &quot;'&quot; , &quot;, error);
            });
    }

    function displayperfilmenu() { 
        var menu = document.getElementById(&quot; , &quot;'&quot; , &quot;perfil-menu&quot; , &quot;'&quot; , &quot;);
        menu.classList.toggle(&quot; , &quot;'&quot; , &quot;display-none&quot; , &quot;'&quot; , &quot;);
    }

                
            
        
    
    
        
            






    
            
                
                   
                    
                        Ordenar ↓ 
                        
                            Preço Descendente
                            Preço Ascendente
                            Marca Ascendente
                            Marca Descendente
                        
                    
                     

                   
                
                
                    
                        
                            Filtrar Por:
                            
                                Zona:
                                
                                    em progresso
                                    em progresso
                                
                            
                            
                                Comerciante:
                                
                                        
                                            
                                            ComerciantePMK
                                        
                                    
                                        
                                
                            
                            
                                Categoria:
                                
                                        
                                            
                                            Enlatados
                                        
                                        
                                            
                                            Frescos
                                        
                                        
                                            
                                            Biologicos
                                        
                                        
                                            
                                            Congelados
                                        
                                        
                                            
                                            Pastelaria
                                        
                                        
                                            
                                            Talho
                                        
                                        
                                            
                                            Peixaria
                                        
                                        
                                            
                                            Charcutaria
                                        
                                        
                                            
                                            Bebidas
                                        
                                        
                                            
                                            Vegan
                                        
                                        
                                            
                                            Doces
                                        
                                        
                                            
                                            Snacks
                                        
                                        
                                            
                                            BebidasAlcoólicas
                                        


                                
                            
                            
                                Preço:
                                
                                    
                                        
                                            
                                            
                                        
                                        
                                            
                                                Min
                                                
                                            
                                            
                                                Max
                                                
                                            
                                        
                                    
                                
                            
                        
                    
                    
                                
                                    
                                    Atum - ComerciantePMK
                                    Enlatados
                                    Bom Petisco
                                    1.2 €/Unit
                                
                                
                                    
                                    Manteiga - ComerciantePMK
                                    Frescos
                                    Mimosa
                                    2.25 €/Unit
                                
                                
                                    
                                    Manteiga Vegetal - ComerciantePMK
                                    Frescos
                                    Planta
                                    3.3 €/Unit
                                
                                
                                    
                                    Manteiga - ComerciantePMK
                                    Frescos
                                    Gresso
                                    40 €/Unit
                                
                    
                  
                
            
    



    function filtrar() {

        var produtos = document.getElementById(&quot;search-results&quot;).children;

        var categoria = document.getElementsByClassName(&quot;categorias&quot;);
        var categoriasSelected = Array.from(categoria);
        var comerciantes = document.getElementsByClassName(&quot;comerciantes&quot;);
        var comerciantesSelected = Array.from(comerciantes);

        var precoMin = document.getElementById(&quot;fromInput&quot;).value;
        var precoMax = document.getElementById(&quot;toInput&quot;).value;

        for (item of Array.from(comerciantes)) {
            if (item.checked == false) {
                var i = comerciantesSelected.indexOf(item);
                comerciantesSelected.splice(i, 1);
            }
        }

        for (item of Array.from(categorias)) {
            if (item.checked == false) {
                var i = categoriasSelected.indexOf(item);
                categoriasSelected.splice(i, 1);
            }
        }

        for(item of produtos){
            if (parseFloat(item.querySelector(&quot;#price&quot;).textContent) > precoMax) {
                item.style.display = &quot; , &quot;'&quot; , &quot;none&quot; , &quot;'&quot; , &quot;;
            } 
            else if (parseFloat(item.querySelector(&quot;#price&quot;).textContent) &lt; precoMin) {
                item.style.display = &quot; , &quot;'&quot; , &quot;none&quot; , &quot;'&quot; , &quot;;
            } else {
                item.style.display = &quot;flex&quot;;
            }

            if(categoriasSelected.length > 0){
                var b = false;
                for(cat of categoriasSelected){
                    if (item.textContent.includes(cat.value)){
                        b = true;
                    }
                }
                if(!b){
                    item.style.display = &quot; , &quot;'&quot; , &quot;none&quot; , &quot;'&quot; , &quot;;
                }
            }
            
            if (comerciantesSelected.length > 0) {
                var b = false;
                for (com of comerciantesSelected) {
                    if (item.textContent.includes(com.value)) {
                        b = true;
                    }
                }
                if (!b) {
                    item.style.display = &quot; , &quot;'&quot; , &quot;none&quot; , &quot;'&quot; , &quot;;
                }
            }
            
        }

    }



    //slider
    function controlFromInput(fromSlider, fromInput, toInput, controlSlider) {
        const [from, to] = getParsed(fromInput, toInput);
        fillSlider(fromInput, toInput, &quot; , &quot;'&quot; , &quot;#C6C6C6&quot; , &quot;'&quot; , &quot;, &quot; , &quot;'&quot; , &quot;#25daa5&quot; , &quot;'&quot; , &quot;, controlSlider);
        if (from > to) {
            fromSlider.value = to;
            fromInput.value = to;
        } else {
            fromSlider.value = from;
        }
    }

    function controlToInput(toSlider, fromInput, toInput, controlSlider) {
        const [from, to] = getParsed(fromInput, toInput);
        fillSlider(fromInput, toInput, &quot; , &quot;'&quot; , &quot;#C6C6C6&quot; , &quot;'&quot; , &quot;, &quot; , &quot;'&quot; , &quot;#25daa5&quot; , &quot;'&quot; , &quot;, controlSlider);
        setToggleAccessible(toInput);
        if (from &lt;= to) {
            toSlider.value = to;
            toInput.value = to;
        } else {
            toInput.value = from;
        }
    }

    function controlFromSlider(fromSlider, toSlider, fromInput) {
        const [from, to] = getParsed(fromSlider, toSlider);
        fillSlider(fromSlider, toSlider, &quot; , &quot;'&quot; , &quot;#C6C6C6&quot; , &quot;'&quot; , &quot;, &quot; , &quot;'&quot; , &quot;#25daa5&quot; , &quot;'&quot; , &quot;, toSlider);
        if (from > to) {
            fromSlider.value = to;
            fromInput.value = to;
        } else {
            fromInput.value = from;
        }
    }

    function controlToSlider(fromSlider, toSlider, toInput) {
        const [from, to] = getParsed(fromSlider, toSlider);
        fillSlider(fromSlider, toSlider, &quot; , &quot;'&quot; , &quot;#C6C6C6&quot; , &quot;'&quot; , &quot;, &quot; , &quot;'&quot; , &quot;#25daa5&quot; , &quot;'&quot; , &quot;, toSlider);
        setToggleAccessible(toSlider);
        if (from &lt;= to) {
            toSlider.value = to;
            toInput.value = to;
        } else {
            toInput.value = from;
            toSlider.value = from;
        }
    }

    function getParsed(currentFrom, currentTo) {
        const from = parseFloat(currentFrom.value, 10);
        const to = parseFloat(currentTo.value, 10);
        return [from, to];
    }

    function fillSlider(from, to, sliderColor, rangeColor, controlSlider) {
        const rangeDistance = to.max - to.min;
        const fromPosition = from.value - to.min;
        const toPosition = to.value - to.min;
        controlSlider.style.background = `linear-gradient(
          to right,
          ${sliderColor} 0%,
          ${sliderColor} ${(fromPosition) / (rangeDistance) * 100}%,
          ${rangeColor} ${((fromPosition) / (rangeDistance)) * 100}%,
          ${rangeColor} ${(toPosition) / (rangeDistance) * 100}%,
          ${sliderColor} ${(toPosition) / (rangeDistance) * 100}%,
          ${sliderColor} 100%)`;
    }

    function setToggleAccessible(currentTarget) {
        const toSlider = document.querySelector(&quot; , &quot;'&quot; , &quot;#toSlider&quot; , &quot;'&quot; , &quot;);
        if (Number(currentTarget.value) &lt;= 0) {
            toSlider.style.zIndex = 2;
        } else {
            toSlider.style.zIndex = 0;
        }
    }

    const fromSlider = document.querySelector(&quot; , &quot;'&quot; , &quot;#fromSlider&quot; , &quot;'&quot; , &quot;);
    const toSlider = document.querySelector(&quot; , &quot;'&quot; , &quot;#toSlider&quot; , &quot;'&quot; , &quot;);
    const fromInput = document.querySelector(&quot; , &quot;'&quot; , &quot;#fromInput&quot; , &quot;'&quot; , &quot;);
    const toInput = document.querySelector(&quot; , &quot;'&quot; , &quot;#toInput&quot; , &quot;'&quot; , &quot;);
    fillSlider(fromSlider, toSlider, &quot; , &quot;'&quot; , &quot;#C6C6C6&quot; , &quot;'&quot; , &quot;, &quot; , &quot;'&quot; , &quot;#25daa5&quot; , &quot;'&quot; , &quot;, toSlider);
    setToggleAccessible(toSlider);

    fromSlider.oninput = () => controlFromSlider(fromSlider, toSlider, fromInput);
    toSlider.oninput = () => controlToSlider(fromSlider, toSlider, toInput);
    fromInput.oninput = () => controlFromInput(fromSlider, fromInput, toInput, toSlider);
    toInput.oninput = () => controlToInput(toSlider, fromInput, toInput, toSlider);

    //collapsibles

    var coll = document.getElementsByClassName(&quot;collapsible&quot;);
    var i;

    for (i = 0; i &lt; coll.length; i++) {
        coll[i].addEventListener(&quot;click&quot;, function () {
            this.classList.toggle(&quot;active&quot;);
            var content = this.nextElementSibling;
            if (content.style.maxHeight) {
                content.style.maxHeight = null;
            } else {
                content.style.maxHeight = content.scrollHeight + &quot;px&quot;;
            }
        });
    }

    function sortResultsByPriceDesc(){
        var resultsContainer = document.getElementById(&quot;search-results&quot;);

        var results = Array.from(document.getElementById(&quot;search-results&quot;).children);

    
        function sortByPrice(a, b) {
            var priceA = parseFloat(a.getElementById(&quot;price&quot;).textContent.replace(/[^\d.]/g, &quot; , &quot;'&quot; , &quot;&quot; , &quot;'&quot; , &quot;));
            var priceB = parseFloat(b.getElementById(&quot;price&quot;).textContent.replace(/[^\d.]/g, &quot; , &quot;'&quot; , &quot;&quot; , &quot;'&quot; , &quot;));
            return priceB - priceA;
        }
        results.sort(sortByPrice);
        resultsContainer.innerHTML = &quot; , &quot;'&quot; , &quot;&quot; , &quot;'&quot; , &quot;;

        results.forEach(function (result) {
            resultsContainer.appendChild(result);
        }); 
       
    }
    function sortResultsByPriceAsc() {
        var resultsContainer = document.getElementById(&quot;search-results&quot;);

        var results = Array.from(document.getElementById(&quot;search-results&quot;).children);


        function sortByPrice(a, b) {
            var priceA = parseFloat(a.getElementById(&quot;price&quot;).textContent.replace(/[^\d.]/g, &quot; , &quot;'&quot; , &quot;&quot; , &quot;'&quot; , &quot;));
            var priceB = parseFloat(b.getElementById(&quot;price&quot;).textContent.replace(/[^\d.]/g, &quot; , &quot;'&quot; , &quot;&quot; , &quot;'&quot; , &quot;));
            return priceA - priceB;
        }
        results.sort(sortByPrice);
        resultsContainer.innerHTML = &quot; , &quot;'&quot; , &quot;&quot; , &quot;'&quot; , &quot;;

        results.forEach(function (result) {
            resultsContainer.appendChild(result);
        });
    }

    function sortResultsByBrandAsc() {
        var resultsContainer = document.getElementById(&quot;search-results&quot;);
        var results = Array.from(document.getElementById(&quot;search-results&quot;).children);

  

        function sortByBrand(a, b) {
            var brandA = a.getElementById(&quot;brand&quot;).textContent;
            var brandB = b.getElementById(&quot;brand&quot;).textContent;

            return brandA.localeCompare(brandB);
        }

        results.sort(sortByBrand);
        resultsContainer.innerHTML = &quot; , &quot;'&quot; , &quot;&quot; , &quot;'&quot; , &quot;;

        results.forEach(function (result) {
            resultsContainer.appendChild(result);
        });
    }
    function sortResultsByBrandDesc() {
        var resultsContainer = document.getElementById(&quot;search-results&quot;);
        var results = Array.from(document.getElementById(&quot;search-results&quot;).children);

      

        function sortByBrand(a, b) {
            var brandA = a.getElementById(&quot;brand&quot;).textContent;
            var brandB = b.getElementById(&quot;brand&quot;).textContent;

            return brandB.localeCompare(brandA);
        }

        results.sort(sortByBrand);
        resultsContainer.innerHTML = &quot; , &quot;'&quot; , &quot;&quot; , &quot;'&quot; , &quot;;

        results.forEach(function (result) {
            resultsContainer.appendChild(result);
        });
    }


        
    

    
    
    
    


/html[1]/body[1]&quot;) or . = concat(&quot;
    
        
            
                

                     PechinchaMarket

                
                
                    
                
                
                    

    
        
            
                
                
            
            
        
    
    
    
            
                Register
            
            
                Login
            
        
    


    document.addEventListener(&quot; , &quot;'&quot; , &quot;DOMContentLoaded&quot; , &quot;'&quot; , &quot;, function () {
        fetchPerfilImage();
    });

    function fetchPerfilImage() {
        fetch(&quot; , &quot;'&quot; , &quot;/Search/GetPerfilImage&quot; , &quot;'&quot; , &quot;, {
            method: &quot; , &quot;'&quot; , &quot;GET&quot; , &quot;'&quot; , &quot;,
            headers: {
                &quot; , &quot;'&quot; , &quot;Content-Type&quot; , &quot;'&quot; , &quot;: &quot; , &quot;'&quot; , &quot;application/json&quot; , &quot;'&quot; , &quot;,
                // Add any additional headers if needed
            }
        })
            .then(response => {
                if (!response.ok) {
                    throw new Error(&quot; , &quot;'&quot; , &quot;Network response was not ok&quot; , &quot;'&quot; , &quot;);
                }
                return response.json(); // Assuming the response is in JSON format
            })
            .then(data => {
                const imageSrc = `url(&quot; , &quot;'&quot; , &quot;data:image/jpeg;base64,${data}&quot; , &quot;'&quot; , &quot;)`;

                document.getElementById(&quot; , &quot;'&quot; , &quot;manage&quot; , &quot;'&quot; , &quot;).style.backgroundImage = imageSrc;
            })
            .catch(error => {
                console.error(&quot; , &quot;'&quot; , &quot;There was a problem with the fetch operation:&quot; , &quot;'&quot; , &quot;, error);
            });
    }

    function displayperfilmenu() { 
        var menu = document.getElementById(&quot; , &quot;'&quot; , &quot;perfil-menu&quot; , &quot;'&quot; , &quot;);
        menu.classList.toggle(&quot; , &quot;'&quot; , &quot;display-none&quot; , &quot;'&quot; , &quot;);
    }

                
            
        
    
    
        
            






    
            
                
                   
                    
                        Ordenar ↓ 
                        
                            Preço Descendente
                            Preço Ascendente
                            Marca Ascendente
                            Marca Descendente
                        
                    
                     

                   
                
                
                    
                        
                            Filtrar Por:
                            
                                Zona:
                                
                                    em progresso
                                    em progresso
                                
                            
                            
                                Comerciante:
                                
                                        
                                            
                                            ComerciantePMK
                                        
                                    
                                        
                                
                            
                            
                                Categoria:
                                
                                        
                                            
                                            Enlatados
                                        
                                        
                                            
                                            Frescos
                                        
                                        
                                            
                                            Biologicos
                                        
                                        
                                            
                                            Congelados
                                        
                                        
                                            
                                            Pastelaria
                                        
                                        
                                            
                                            Talho
                                        
                                        
                                            
                                            Peixaria
                                        
                                        
                                            
                                            Charcutaria
                                        
                                        
                                            
                                            Bebidas
                                        
                                        
                                            
                                            Vegan
                                        
                                        
                                            
                                            Doces
                                        
                                        
                                            
                                            Snacks
                                        
                                        
                                            
                                            BebidasAlcoólicas
                                        


                                
                            
                            
                                Preço:
                                
                                    
                                        
                                            
                                            
                                        
                                        
                                            
                                                Min
                                                
                                            
                                            
                                                Max
                                                
                                            
                                        
                                    
                                
                            
                        
                    
                    
                                
                                    
                                    Atum - ComerciantePMK
                                    Enlatados
                                    Bom Petisco
                                    1.2 €/Unit
                                
                                
                                    
                                    Manteiga - ComerciantePMK
                                    Frescos
                                    Mimosa
                                    2.25 €/Unit
                                
                                
                                    
                                    Manteiga Vegetal - ComerciantePMK
                                    Frescos
                                    Planta
                                    3.3 €/Unit
                                
                                
                                    
                                    Manteiga - ComerciantePMK
                                    Frescos
                                    Gresso
                                    40 €/Unit
                                
                    
                  
                
            
    



    function filtrar() {

        var produtos = document.getElementById(&quot;search-results&quot;).children;

        var categoria = document.getElementsByClassName(&quot;categorias&quot;);
        var categoriasSelected = Array.from(categoria);
        var comerciantes = document.getElementsByClassName(&quot;comerciantes&quot;);
        var comerciantesSelected = Array.from(comerciantes);

        var precoMin = document.getElementById(&quot;fromInput&quot;).value;
        var precoMax = document.getElementById(&quot;toInput&quot;).value;

        for (item of Array.from(comerciantes)) {
            if (item.checked == false) {
                var i = comerciantesSelected.indexOf(item);
                comerciantesSelected.splice(i, 1);
            }
        }

        for (item of Array.from(categorias)) {
            if (item.checked == false) {
                var i = categoriasSelected.indexOf(item);
                categoriasSelected.splice(i, 1);
            }
        }

        for(item of produtos){
            if (parseFloat(item.querySelector(&quot;#price&quot;).textContent) > precoMax) {
                item.style.display = &quot; , &quot;'&quot; , &quot;none&quot; , &quot;'&quot; , &quot;;
            } 
            else if (parseFloat(item.querySelector(&quot;#price&quot;).textContent) &lt; precoMin) {
                item.style.display = &quot; , &quot;'&quot; , &quot;none&quot; , &quot;'&quot; , &quot;;
            } else {
                item.style.display = &quot;flex&quot;;
            }

            if(categoriasSelected.length > 0){
                var b = false;
                for(cat of categoriasSelected){
                    if (item.textContent.includes(cat.value)){
                        b = true;
                    }
                }
                if(!b){
                    item.style.display = &quot; , &quot;'&quot; , &quot;none&quot; , &quot;'&quot; , &quot;;
                }
            }
            
            if (comerciantesSelected.length > 0) {
                var b = false;
                for (com of comerciantesSelected) {
                    if (item.textContent.includes(com.value)) {
                        b = true;
                    }
                }
                if (!b) {
                    item.style.display = &quot; , &quot;'&quot; , &quot;none&quot; , &quot;'&quot; , &quot;;
                }
            }
            
        }

    }



    //slider
    function controlFromInput(fromSlider, fromInput, toInput, controlSlider) {
        const [from, to] = getParsed(fromInput, toInput);
        fillSlider(fromInput, toInput, &quot; , &quot;'&quot; , &quot;#C6C6C6&quot; , &quot;'&quot; , &quot;, &quot; , &quot;'&quot; , &quot;#25daa5&quot; , &quot;'&quot; , &quot;, controlSlider);
        if (from > to) {
            fromSlider.value = to;
            fromInput.value = to;
        } else {
            fromSlider.value = from;
        }
    }

    function controlToInput(toSlider, fromInput, toInput, controlSlider) {
        const [from, to] = getParsed(fromInput, toInput);
        fillSlider(fromInput, toInput, &quot; , &quot;'&quot; , &quot;#C6C6C6&quot; , &quot;'&quot; , &quot;, &quot; , &quot;'&quot; , &quot;#25daa5&quot; , &quot;'&quot; , &quot;, controlSlider);
        setToggleAccessible(toInput);
        if (from &lt;= to) {
            toSlider.value = to;
            toInput.value = to;
        } else {
            toInput.value = from;
        }
    }

    function controlFromSlider(fromSlider, toSlider, fromInput) {
        const [from, to] = getParsed(fromSlider, toSlider);
        fillSlider(fromSlider, toSlider, &quot; , &quot;'&quot; , &quot;#C6C6C6&quot; , &quot;'&quot; , &quot;, &quot; , &quot;'&quot; , &quot;#25daa5&quot; , &quot;'&quot; , &quot;, toSlider);
        if (from > to) {
            fromSlider.value = to;
            fromInput.value = to;
        } else {
            fromInput.value = from;
        }
    }

    function controlToSlider(fromSlider, toSlider, toInput) {
        const [from, to] = getParsed(fromSlider, toSlider);
        fillSlider(fromSlider, toSlider, &quot; , &quot;'&quot; , &quot;#C6C6C6&quot; , &quot;'&quot; , &quot;, &quot; , &quot;'&quot; , &quot;#25daa5&quot; , &quot;'&quot; , &quot;, toSlider);
        setToggleAccessible(toSlider);
        if (from &lt;= to) {
            toSlider.value = to;
            toInput.value = to;
        } else {
            toInput.value = from;
            toSlider.value = from;
        }
    }

    function getParsed(currentFrom, currentTo) {
        const from = parseFloat(currentFrom.value, 10);
        const to = parseFloat(currentTo.value, 10);
        return [from, to];
    }

    function fillSlider(from, to, sliderColor, rangeColor, controlSlider) {
        const rangeDistance = to.max - to.min;
        const fromPosition = from.value - to.min;
        const toPosition = to.value - to.min;
        controlSlider.style.background = `linear-gradient(
          to right,
          ${sliderColor} 0%,
          ${sliderColor} ${(fromPosition) / (rangeDistance) * 100}%,
          ${rangeColor} ${((fromPosition) / (rangeDistance)) * 100}%,
          ${rangeColor} ${(toPosition) / (rangeDistance) * 100}%,
          ${sliderColor} ${(toPosition) / (rangeDistance) * 100}%,
          ${sliderColor} 100%)`;
    }

    function setToggleAccessible(currentTarget) {
        const toSlider = document.querySelector(&quot; , &quot;'&quot; , &quot;#toSlider&quot; , &quot;'&quot; , &quot;);
        if (Number(currentTarget.value) &lt;= 0) {
            toSlider.style.zIndex = 2;
        } else {
            toSlider.style.zIndex = 0;
        }
    }

    const fromSlider = document.querySelector(&quot; , &quot;'&quot; , &quot;#fromSlider&quot; , &quot;'&quot; , &quot;);
    const toSlider = document.querySelector(&quot; , &quot;'&quot; , &quot;#toSlider&quot; , &quot;'&quot; , &quot;);
    const fromInput = document.querySelector(&quot; , &quot;'&quot; , &quot;#fromInput&quot; , &quot;'&quot; , &quot;);
    const toInput = document.querySelector(&quot; , &quot;'&quot; , &quot;#toInput&quot; , &quot;'&quot; , &quot;);
    fillSlider(fromSlider, toSlider, &quot; , &quot;'&quot; , &quot;#C6C6C6&quot; , &quot;'&quot; , &quot;, &quot; , &quot;'&quot; , &quot;#25daa5&quot; , &quot;'&quot; , &quot;, toSlider);
    setToggleAccessible(toSlider);

    fromSlider.oninput = () => controlFromSlider(fromSlider, toSlider, fromInput);
    toSlider.oninput = () => controlToSlider(fromSlider, toSlider, toInput);
    fromInput.oninput = () => controlFromInput(fromSlider, fromInput, toInput, toSlider);
    toInput.oninput = () => controlToInput(toSlider, fromInput, toInput, toSlider);

    //collapsibles

    var coll = document.getElementsByClassName(&quot;collapsible&quot;);
    var i;

    for (i = 0; i &lt; coll.length; i++) {
        coll[i].addEventListener(&quot;click&quot;, function () {
            this.classList.toggle(&quot;active&quot;);
            var content = this.nextElementSibling;
            if (content.style.maxHeight) {
                content.style.maxHeight = null;
            } else {
                content.style.maxHeight = content.scrollHeight + &quot;px&quot;;
            }
        });
    }

    function sortResultsByPriceDesc(){
        var resultsContainer = document.getElementById(&quot;search-results&quot;);

        var results = Array.from(document.getElementById(&quot;search-results&quot;).children);

    
        function sortByPrice(a, b) {
            var priceA = parseFloat(a.getElementById(&quot;price&quot;).textContent.replace(/[^\d.]/g, &quot; , &quot;'&quot; , &quot;&quot; , &quot;'&quot; , &quot;));
            var priceB = parseFloat(b.getElementById(&quot;price&quot;).textContent.replace(/[^\d.]/g, &quot; , &quot;'&quot; , &quot;&quot; , &quot;'&quot; , &quot;));
            return priceB - priceA;
        }
        results.sort(sortByPrice);
        resultsContainer.innerHTML = &quot; , &quot;'&quot; , &quot;&quot; , &quot;'&quot; , &quot;;

        results.forEach(function (result) {
            resultsContainer.appendChild(result);
        }); 
       
    }
    function sortResultsByPriceAsc() {
        var resultsContainer = document.getElementById(&quot;search-results&quot;);

        var results = Array.from(document.getElementById(&quot;search-results&quot;).children);


        function sortByPrice(a, b) {
            var priceA = parseFloat(a.getElementById(&quot;price&quot;).textContent.replace(/[^\d.]/g, &quot; , &quot;'&quot; , &quot;&quot; , &quot;'&quot; , &quot;));
            var priceB = parseFloat(b.getElementById(&quot;price&quot;).textContent.replace(/[^\d.]/g, &quot; , &quot;'&quot; , &quot;&quot; , &quot;'&quot; , &quot;));
            return priceA - priceB;
        }
        results.sort(sortByPrice);
        resultsContainer.innerHTML = &quot; , &quot;'&quot; , &quot;&quot; , &quot;'&quot; , &quot;;

        results.forEach(function (result) {
            resultsContainer.appendChild(result);
        });
    }

    function sortResultsByBrandAsc() {
        var resultsContainer = document.getElementById(&quot;search-results&quot;);
        var results = Array.from(document.getElementById(&quot;search-results&quot;).children);

  

        function sortByBrand(a, b) {
            var brandA = a.getElementById(&quot;brand&quot;).textContent;
            var brandB = b.getElementById(&quot;brand&quot;).textContent;

            return brandA.localeCompare(brandB);
        }

        results.sort(sortByBrand);
        resultsContainer.innerHTML = &quot; , &quot;'&quot; , &quot;&quot; , &quot;'&quot; , &quot;;

        results.forEach(function (result) {
            resultsContainer.appendChild(result);
        });
    }
    function sortResultsByBrandDesc() {
        var resultsContainer = document.getElementById(&quot;search-results&quot;);
        var results = Array.from(document.getElementById(&quot;search-results&quot;).children);

      

        function sortByBrand(a, b) {
            var brandA = a.getElementById(&quot;brand&quot;).textContent;
            var brandB = b.getElementById(&quot;brand&quot;).textContent;

            return brandB.localeCompare(brandA);
        }

        results.sort(sortByBrand);
        resultsContainer.innerHTML = &quot; , &quot;'&quot; , &quot;&quot; , &quot;'&quot; , &quot;;

        results.forEach(function (result) {
            resultsContainer.appendChild(result);
        });
    }


        
    

    
    
    
    


/html[1]/body[1]&quot;))]</value>
      <webElementGuid>3a06c706-c85f-42cb-bc39-8cac1e1e7a65</webElementGuid>
   </webElementXpaths>
</WebElementEntity>
