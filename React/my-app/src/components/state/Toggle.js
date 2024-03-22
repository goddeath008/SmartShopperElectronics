import React, { useState } from 'react';

const Toggle = () => {
    const [On, setOn] = useState(false);
    console.log(On);
    return (
        <div className='Toggle' onClick={()=> setOn((On)=>!On)}>
            Toggle {On ? "On":"Off"}
        </div>
    );
};

export default Toggle;