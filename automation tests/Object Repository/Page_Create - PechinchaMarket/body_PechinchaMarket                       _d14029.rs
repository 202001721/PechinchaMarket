<?xml version="1.0" encoding="UTF-8"?>
<WebElementEntity>
   <description></description>
   <name>body_PechinchaMarket                       _d14029</name>
   <tag></tag>
   <elementGuidId>180fb617-4fdf-4b66-a5cc-b9f9d53a4002</elementGuidId>
   <selectorCollection>
      <entry>
         <key>XPATH</key>
         <value>//body</value>
      </entry>
      <entry>
         <key>CSS</key>
         <value>body</value>
      </entry>
   </selectorCollection>
   <selectorMethod>XPATH</selectorMethod>
   <smartLocatorCollection>
      <entry>
         <key>SMART_LOCATOR</key>
         <value>body</value>
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
      <webElementGuid>fcbd0a58-ccda-4d08-a795-2250424c1add</webElementGuid>
   </webElementProperties>
   <webElementProperties>
      <isSelected>true</isSelected>
      <matchCondition>equals</matchCondition>
      <name>text</name>
      <type>Main</type>
      <value>
    
        
            
                

                     PechinchaMarket

                
                
                    



    
        
        
    
    


            
                ?
                
                    Balão Informativo
                    
                        
                    
                        Fechar
                    
                
            
            Produtos
            Lojas
        
            
                
                    Perfil
                    
                        Terminar Sessão
                    
                
            
        



    document.addEventListener('click', function (event) {
        var targetDiv1 = document.getElementById(&quot;manage&quot;);
        var targetDiv2 = document.getElementById(&quot;tooltip-icon&quot;);
        var enableDiv = document.getElementById(&quot;tooltip-message&quot;);


        if (!enableDiv.classList.contains(&quot;display-none&quot;) &amp;&amp; !(targetDiv2.contains(event.target) || enableDiv.contains(event.target))){
            toggleTooltipMessage();
        }

        if (!targetDiv1.children[0].classList.contains(&quot;display-none&quot;) &amp;&amp; !targetDiv1.contains(event.target)) {
            displayperfilmenu();
        }
    });

    function toggleTooltipMessage(){ 
        var tooltip = document.getElementById(&quot;tooltip-message&quot;);
        tooltip.classList.toggle(&quot;display-none&quot;);
    }

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

                
                
                    
                
            
        
    
    
        
            




    function initAutocomplete() {
        var input = document.getElementById('location-input');
        var autocomplete = new google.maps.places.Autocomplete(input, {
            types: ['geocode'], // Optional: restricts the search to addresses
            componentRestrictions: { country: 'PT' }, // Restricts the results to Portugal
            language: 'pt' // Set language to Portuguese
        });
        autocomplete.setFields(['address_components', 'formatted_address']);
    }



Adicionar Loja

    
        
            
            
                Morada
                
                
            
            
                Horário de Abertura
                
                
            
            
                Horário de Fecho
                
                
            
            
                
            
               
        
    



        
    

    
    
    
    







    function toggleNavMenu(div) { 
        div.classList.toggle(&quot;nav-opened&quot;);
    }
/html[1]/body[1]LisboaPortugalLisboa RegionPortugalLisboa - CidadeLisbon, PortugalRua LisboaEstoril, PortugalRua de LisboaBeja, Portugal</value>
      <webElementGuid>396377a3-5396-4b2d-a473-c8afcbb59936</webElementGuid>
   </webElementProperties>
   <webElementProperties>
      <isSelected>false</isSelected>
      <matchCondition>equals</matchCondition>
      <name>xpath</name>
      <type>Main</type>
      <value>/html[1]/body[1]</value>
      <webElementGuid>0139793d-12d1-4500-aec1-a45baf41c63c</webElementGuid>
   </webElementProperties>
   <webElementXpaths>
      <isSelected>true</isSelected>
      <matchCondition>equals</matchCondition>
      <name>xpath:position</name>
      <type>Main</type>
      <value>//body</value>
      <webElementGuid>dece8943-aa84-4831-b7cb-bd18751a1d65</webElementGuid>
   </webElementXpaths>
   <webElementXpaths>
      <isSelected>false</isSelected>
      <matchCondition>equals</matchCondition>
      <name>xpath:customAttributes</name>
      <type>Main</type>
      <value>//body[(text() = concat(&quot;
    
        
            
                

                     PechinchaMarket

                
                
                    



    
        
        
    
    


            
                ?
                
                    Balão Informativo
                    
                        
                    
                        Fechar
                    
                
            
            Produtos
            Lojas
        
            
                
                    Perfil
                    
                        Terminar Sessão
                    
                
            
        



    document.addEventListener(&quot; , &quot;'&quot; , &quot;click&quot; , &quot;'&quot; , &quot;, function (event) {
        var targetDiv1 = document.getElementById(&quot;manage&quot;);
        var targetDiv2 = document.getElementById(&quot;tooltip-icon&quot;);
        var enableDiv = document.getElementById(&quot;tooltip-message&quot;);


        if (!enableDiv.classList.contains(&quot;display-none&quot;) &amp;&amp; !(targetDiv2.contains(event.target) || enableDiv.contains(event.target))){
            toggleTooltipMessage();
        }

        if (!targetDiv1.children[0].classList.contains(&quot;display-none&quot;) &amp;&amp; !targetDiv1.contains(event.target)) {
            displayperfilmenu();
        }
    });

    function toggleTooltipMessage(){ 
        var tooltip = document.getElementById(&quot;tooltip-message&quot;);
        tooltip.classList.toggle(&quot;display-none&quot;);
    }

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

                
                
                    
                
            
        
    
    
        
            




    function initAutocomplete() {
        var input = document.getElementById(&quot; , &quot;'&quot; , &quot;location-input&quot; , &quot;'&quot; , &quot;);
        var autocomplete = new google.maps.places.Autocomplete(input, {
            types: [&quot; , &quot;'&quot; , &quot;geocode&quot; , &quot;'&quot; , &quot;], // Optional: restricts the search to addresses
            componentRestrictions: { country: &quot; , &quot;'&quot; , &quot;PT&quot; , &quot;'&quot; , &quot; }, // Restricts the results to Portugal
            language: &quot; , &quot;'&quot; , &quot;pt&quot; , &quot;'&quot; , &quot; // Set language to Portuguese
        });
        autocomplete.setFields([&quot; , &quot;'&quot; , &quot;address_components&quot; , &quot;'&quot; , &quot;, &quot; , &quot;'&quot; , &quot;formatted_address&quot; , &quot;'&quot; , &quot;]);
    }



Adicionar Loja

    
        
            
            
                Morada
                
                
            
            
                Horário de Abertura
                
                
            
            
                Horário de Fecho
                
                
            
            
                
            
               
        
    



        
    

    
    
    
    







    function toggleNavMenu(div) { 
        div.classList.toggle(&quot;nav-opened&quot;);
    }
/html[1]/body[1]LisboaPortugalLisboa RegionPortugalLisboa - CidadeLisbon, PortugalRua LisboaEstoril, PortugalRua de LisboaBeja, Portugal&quot;) or . = concat(&quot;
    
        
            
                

                     PechinchaMarket

                
                
                    



    
        
        
    
    


            
                ?
                
                    Balão Informativo
                    
                        
                    
                        Fechar
                    
                
            
            Produtos
            Lojas
        
            
                
                    Perfil
                    
                        Terminar Sessão
                    
                
            
        



    document.addEventListener(&quot; , &quot;'&quot; , &quot;click&quot; , &quot;'&quot; , &quot;, function (event) {
        var targetDiv1 = document.getElementById(&quot;manage&quot;);
        var targetDiv2 = document.getElementById(&quot;tooltip-icon&quot;);
        var enableDiv = document.getElementById(&quot;tooltip-message&quot;);


        if (!enableDiv.classList.contains(&quot;display-none&quot;) &amp;&amp; !(targetDiv2.contains(event.target) || enableDiv.contains(event.target))){
            toggleTooltipMessage();
        }

        if (!targetDiv1.children[0].classList.contains(&quot;display-none&quot;) &amp;&amp; !targetDiv1.contains(event.target)) {
            displayperfilmenu();
        }
    });

    function toggleTooltipMessage(){ 
        var tooltip = document.getElementById(&quot;tooltip-message&quot;);
        tooltip.classList.toggle(&quot;display-none&quot;);
    }

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

                
                
                    
                
            
        
    
    
        
            




    function initAutocomplete() {
        var input = document.getElementById(&quot; , &quot;'&quot; , &quot;location-input&quot; , &quot;'&quot; , &quot;);
        var autocomplete = new google.maps.places.Autocomplete(input, {
            types: [&quot; , &quot;'&quot; , &quot;geocode&quot; , &quot;'&quot; , &quot;], // Optional: restricts the search to addresses
            componentRestrictions: { country: &quot; , &quot;'&quot; , &quot;PT&quot; , &quot;'&quot; , &quot; }, // Restricts the results to Portugal
            language: &quot; , &quot;'&quot; , &quot;pt&quot; , &quot;'&quot; , &quot; // Set language to Portuguese
        });
        autocomplete.setFields([&quot; , &quot;'&quot; , &quot;address_components&quot; , &quot;'&quot; , &quot;, &quot; , &quot;'&quot; , &quot;formatted_address&quot; , &quot;'&quot; , &quot;]);
    }



Adicionar Loja

    
        
            
            
                Morada
                
                
            
            
                Horário de Abertura
                
                
            
            
                Horário de Fecho
                
                
            
            
                
            
               
        
    



        
    

    
    
    
    







    function toggleNavMenu(div) { 
        div.classList.toggle(&quot;nav-opened&quot;);
    }
/html[1]/body[1]LisboaPortugalLisboa RegionPortugalLisboa - CidadeLisbon, PortugalRua LisboaEstoril, PortugalRua de LisboaBeja, Portugal&quot;))]</value>
      <webElementGuid>1e46751f-1546-40dc-a8f2-8a4f8f34699c</webElementGuid>
   </webElementXpaths>
</WebElementEntity>
