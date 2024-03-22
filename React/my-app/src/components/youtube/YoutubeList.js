import React from 'react';
import YoutubeItem from './YoutubeItem';
import { YoutubeData } from '../../Data';


const YoutubeList = (props) => {
    return (
        <div>
        <div class="youtube-list">
        {props.children}
        {YoutubeData.map((item, index) =>(
        <YoutubeItem 
        key={item.id}
       image={item.image}
       title={item.title}

       />
       ))}
        </div>
           
        </div>
    );
};

export default YoutubeList;